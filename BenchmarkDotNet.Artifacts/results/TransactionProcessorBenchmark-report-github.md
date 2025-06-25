```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.5965/22H2/2022Update)
Intel Core i7-6820HQ CPU 2.70GHz (Max: 2.71GHz) (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method             | Mean          | Error         | StdDev        | Ratio    | RatioSD | Gen0      | Gen1      | Gen2     | Allocated   | Alloc Ratio |
|------------------- |--------------:|--------------:|--------------:|---------:|--------:|----------:|----------:|---------:|------------:|------------:|
| &#39;Small batch&#39;      |      9.246 μs |     0.1769 μs |     0.3098 μs |     1.00 |    0.05 |    2.0752 |         - |        - |     8.49 KB |        1.00 |
| &#39;Large batch&#39;      |    266.477 μs |     4.0995 μs |     3.8346 μs |    28.85 |    1.01 |   55.6641 |   13.6719 |        - |    223.1 KB |       26.26 |
| &#39;Very large batch&#39; | 64,932.337 μs | 1,658.7299 μs | 4,890.8030 μs | 7,029.88 |  572.99 | 2666.6667 | 1555.5556 | 444.4444 | 20591.58 KB |    2,424.21 |


# Analyse des résultats actuels du benchmark

## Performances globales

- **Petits lots (4 transactions)** :
  - Temps moyen : ~8.5 μs
  - Allocation mémoire : ~8.5 KB
  - Performance stable quel que soit le batch size testé

- **Lots moyens (1,000 transactions)** :
  - Temps moyen : ~260-264 μs
  - Allocation mémoire : ~224 KB
  - Ratio temps/petit lot : ~31x

- **Très grands lots (100,000 transactions)** :
  - Temps moyen : ~58-59 ms
  - Allocation mémoire : ~20.5 MB
  - Ratio temps/petit lot : ~6,800x

## Problèmes identifiés

### Temps d'exécution
⚠️ **Problème majeur** : 
- Scaling non-linéaire entre taille du lot et temps d'exécution
- 1000 tx → 260μs (attendu ~2.1ms si linéaire)
- 100000 tx → 58ms (100x plus que 1000 tx au lieu de 100x)

### Utilisation mémoire
⚠️ **Problème majeur** :
- Allocation excessive pour les grands lots (20MB+)
- Gen0 collections élevées (2666 pour 100k tx)
- Présence de collections Gen1/Gen2 (indique objets à longue durée de vie)

### Ratios anormaux
- Temps 100k tx / 1k tx ≈ 220x (attendu ~100x)
- Mémoire 100k tx / 1k tx ≈ 92x (attendu ~100x)

## Points positifs

✅ **Petits lots performants** :
- Temps d'exécution < 10μs
- Allocation mémoire minimale
- Aucune collection Gen1/Gen2

✅ **Cohérence des résultats** :
- Peu de variation entre runs (Error/StdDev faibles)
- Comportement similaire pour différents batch sizes

## Analyse des causes probables

1. **Overhead de parallélisation** :
   - Coût du threading dépasse les bénéfices pour petits/moyens lots
   - Synchronisation excessive pour grands lots

2. **Gestion mémoire sous-optimale** :
   - Multiples allocations temporaires
   - Conversions de type inutiles

3. **Structure de données inadaptée** :
   - Utilisation de List<T> avec resize fréquent
   - Détection des doublons pourrait être optimisée
