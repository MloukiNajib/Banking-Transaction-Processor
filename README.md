
# Example Problem: Optimizing a Banking Transaction Processor
## Context:
You are given a .NET (C#) application that processes a large batch of financial transactions (e.g., payments, trades, or settlements). The current implementation is slow and inefficient, and your task is to optimize it for performance, memory usage, and correctness.

### Initial (Non-Optimized) 
- Code first commit/dc89f8226111817b6159990e8bfd179593b05ce6

### Optimized Code
- commit/b2eaf3aec370a7d43b1075d390303984aee92df6

Améliorations apportées :

1- Structure du programme :
- Séparation claire entre la méthode Main et les fonctions auxiliaires
- Extraction de la création des transactions dans une méthode dédiée
- Extraction de l'affichage dans une méthode dédiée

2- TransactionProcessor :
- Séparation des responsabilités en méthodes distinctes
- Validation des transactions séparée de la conversion
- Création d'une méthode dédiée pour la création de transaction en euros

3- Lisibilité :
- Noms de méthodes plus explicites
- Meilleure organisation du code
- Moins de commentaires car le code est auto-explicatif

4- Petites améliorations :
- Utilisation de Thread.Sleep au lieu de System.Threading.Thread.Sleep
- Message d'erreur plus clair pour les devises non supportées
- Utilisation de IEnumerable pour l'affichage des transactions traitées

Le code fait exactement la même chose mais est mieux organisé et plus facile à maintenir.


