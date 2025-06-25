
# Example Problem: Optimizing a Banking Transaction Processor
## Context:
You are given a .NET (C#) application that processes a large batch of financial transactions (e.g., payments, trades, or settlements). The current implementation is slow and inefficient, and your task is to optimize it for performance, memory usage, and correctness.

### Initial (Non-Optimized) 
- Code first commit/dc89f8226111817b6159990e8bfd179593b05ce6

### Optimized Code
- commit/b2eaf3aec370a7d43b1075d390303984aee92df6

Am�liorations apport�es :

1- Structure du programme :
- S�paration claire entre la m�thode Main et les fonctions auxiliaires
- Extraction de la cr�ation des transactions dans une m�thode d�di�e
- Extraction de l'affichage dans une m�thode d�di�e

2- TransactionProcessor :
- S�paration des responsabilit�s en m�thodes distinctes
- Validation des transactions s�par�e de la conversion
- Cr�ation d'une m�thode d�di�e pour la cr�ation de transaction en euros

3- Lisibilit� :
- Noms de m�thodes plus explicites
- Meilleure organisation du code
- Moins de commentaires car le code est auto-explicatif

4- Petites am�liorations :
- Utilisation de Thread.Sleep au lieu de System.Threading.Thread.Sleep
- Message d'erreur plus clair pour les devises non support�es
- Utilisation de IEnumerable pour l'affichage des transactions trait�es

Le code fait exactement la m�me chose mais est mieux organis� et plus facile � maintenir.


