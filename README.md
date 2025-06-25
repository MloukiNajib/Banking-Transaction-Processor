
# Example Problem: Optimizing a Banking Transaction Processor
## Context:
You are given a .NET (C#) application that processes a large batch of financial transactions (e.g., payments, trades, or settlements). The current implementation is slow and inefficient, and your task is to optimize it for performance, memory usage, and correctness.

### 1. Initial (Non-Optimized) 
    => Code first commit/dc89f8226111817b6159990e8bfd179593b05ce6

### 2. R�organisation : Structure et Lisibilit�
    => commit/b2eaf3aec370a7d43b1075d390303984aee92df6

- Extraction des m�thodes pour s�paration des responsabilit�s
- Am�lioration des noms de variables et m�thodes
- Optimisation de l'organisation du code

Le code fait exactement la m�me chose mais est mieux organis� et plus facile � maintenir.

### 3. Analyse des points critiques
- Dans votre code, plusieurs points m�ritent une attention particuli�re :
- ConvertCurrency : La m�thode simule un appel API lent avec Thread.Sleep(10). C'est clairement un goulot d'�tranglement.
- Validation des transactions : La v�rification des doublons avec HashSet est efficace, mais pourrait �tre optimis�e pour de tr�s grandes collections.
- Conversion de devises : Les op�rations sont synchrones et bloquantes.

### 4. Suggestions d'am�lioration
 1. Optimisation de ConvertCurrency
 1. Parall�lisation du traitement
 1. Approche asynchrone

### 5. Outils d'analyse avanc�s
1. Visual Studio Profiler : Pour une analyse d�taill�e de l'utilisation CPU et m�moire
1. JetBrains dotTrace/dotMemory : Outils puissants pour le profiling
1. Application Insights : Pour la t�l�m�trie en production
1. BenchmarkDotNet : Pour mesurer les performances des m�thodes
1. NDepend : Pour l'analyse statique du code et la d�tection des probl�mes de performance
1. Roslyn Analyzers : Pour des r�gles de codage personnalis�es et l'analyse statique
1. FxCop : Pour l'analyse de la qualit� du code et des performances
1. SonarQube : Pour l'analyse continue de la qualit� du code
1. ReSharper : Pour l'analyse de code et les suggestions d'am�lioration
1. PerfView : Pour l'analyse des performances et la collecte de traces
1. Roslynator : Pour l'analyse de code et les suggestions d'am�lioration
1. CodeRush : Pour l'analyse de code et les suggestions d'am�lioration
1. T�l�m�trie avec Stopwatch :
   - Utilisation de `Stopwatch` pour mesurer le temps d'ex�cution des m�thodes critiques.
   - Ajout de logs pour suivre les performances en production.


### 6. Benchmarking avec BenchmarkDotNet
    La meilleure approche pour mesurer les performances pr�cises de votre code est d'utiliser BenchmarkDotNet, un framework de benchmarking puissant pour .NET.

    1. Ajoutez le package NuGet : dotnet add package BenchmarkDotNet
    2. Cr�ez une classe de benchmark : TransactionProcessorBenchmark.cs
    3. Ex�cutez le benchmark : dotnet run -c Release
    
    
### 7. Limitations et consid�rations BenchMark tool
    Affichage dans la m�thode benchmark : 
    - TransactionData.DisplayProcessedTransactions et Console.ReadLine() dans la m�thode benchmark faussent les mesures
    - Les op�rations d'I/O (Console) ne devraient pas �tre incluses dans le benchmark
    Donn�es de test limit�es :
    - Seulement 4 transactions dans les �chantillons
    - Pas de cas de test pour transactions invalides/doublons
    Sleep dans ConvertCurrency :
    - Le Thread.Sleep(10) simule un appel API lent mais rend le benchmark peu repr�sentatif des optimisations

### 8. Analyse des Optimisations

1. Suppression du Thread.Sleep :
- Le temps de traitement est maintenant r�aliste (ns/�s au lieu de ms)
- Permet de mesurer les vraies performances de l'algorithme

2. Utilisation de Dictionary pour les taux de change :
- Recherche en O(1) au lieu du switch
- Meilleure maintenabilit� pour ajouter de nouvelles devises

3. Initialisation des listes avec capacit� :
- new List<Transaction>(transactions.Count) r�duit les r�allocations
- Visible dans la r�duction de l'allocation m�moire

