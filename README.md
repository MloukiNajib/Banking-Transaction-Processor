
# Example Problem: Optimizing a Banking Transaction Processor
## Context:
You are given a .NET (C#) application that processes a large batch of financial transactions (e.g., payments, trades, or settlements). The current implementation is slow and inefficient, and your task is to optimize it for performance, memory usage, and correctness.

### 1. Initial (Non-Optimized) 
    => Code first commit/dc89f8226111817b6159990e8bfd179593b05ce6

### 2. Réorganisation : Structure et Lisibilité
    => commit/b2eaf3aec370a7d43b1075d390303984aee92df6

- Extraction des méthodes pour séparation des responsabilités
- Amélioration des noms de variables et méthodes
- Optimisation de l'organisation du code

Le code fait exactement la même chose mais est mieux organisé et plus facile à maintenir.

### 3. Analyse des points critiques
- Dans votre code, plusieurs points méritent une attention particulière :
- ConvertCurrency : La méthode simule un appel API lent avec Thread.Sleep(10). C'est clairement un goulot d'étranglement.
- Validation des transactions : La vérification des doublons avec HashSet est efficace, mais pourrait être optimisée pour de très grandes collections.
- Conversion de devises : Les opérations sont synchrones et bloquantes.

### 4. Suggestions d'amélioration
 1. Optimisation de ConvertCurrency
 1. Parallélisation du traitement
 1. Approche asynchrone

### 5. Outils d'analyse avancés
1. Visual Studio Profiler : Pour une analyse détaillée de l'utilisation CPU et mémoire
1. JetBrains dotTrace/dotMemory : Outils puissants pour le profiling
1. Application Insights : Pour la télémétrie en production
1. BenchmarkDotNet : Pour mesurer les performances des méthodes
1. NDepend : Pour l'analyse statique du code et la détection des problèmes de performance
1. Roslyn Analyzers : Pour des règles de codage personnalisées et l'analyse statique
1. FxCop : Pour l'analyse de la qualité du code et des performances
1. SonarQube : Pour l'analyse continue de la qualité du code
1. ReSharper : Pour l'analyse de code et les suggestions d'amélioration
1. PerfView : Pour l'analyse des performances et la collecte de traces
1. Roslynator : Pour l'analyse de code et les suggestions d'amélioration
1. CodeRush : Pour l'analyse de code et les suggestions d'amélioration
1. Télémétrie avec Stopwatch :
   - Utilisation de `Stopwatch` pour mesurer le temps d'exécution des méthodes critiques.
   - Ajout de logs pour suivre les performances en production.


### 6. Benchmarking avec BenchmarkDotNet
    La meilleure approche pour mesurer les performances précises de votre code est d'utiliser BenchmarkDotNet, un framework de benchmarking puissant pour .NET.

    1. Ajoutez le package NuGet : dotnet add package BenchmarkDotNet
    2. Créez une classe de benchmark : TransactionProcessorBenchmark.cs
    3. Exécutez le benchmark : dotnet run -c Release
    
    
### 7. Limitations et considérations BenchMark tool
    Affichage dans la méthode benchmark : 
    - TransactionData.DisplayProcessedTransactions et Console.ReadLine() dans la méthode benchmark faussent les mesures
    - Les opérations d'I/O (Console) ne devraient pas être incluses dans le benchmark
    Données de test limitées :
    - Seulement 4 transactions dans les échantillons
    - Pas de cas de test pour transactions invalides/doublons
    Sleep dans ConvertCurrency :
    - Le Thread.Sleep(10) simule un appel API lent mais rend le benchmark peu représentatif des optimisations

### 8. Analyse des Optimisations

1. Suppression du Thread.Sleep :
- Le temps de traitement est maintenant réaliste (ns/µs au lieu de ms)
- Permet de mesurer les vraies performances de l'algorithme

2. Utilisation de Dictionary pour les taux de change :
- Recherche en O(1) au lieu du switch
- Meilleure maintenabilité pour ajouter de nouvelles devises

3. Initialisation des listes avec capacité :
- new List<Transaction>(transactions.Count) réduit les réallocations
- Visible dans la réduction de l'allocation mémoire

