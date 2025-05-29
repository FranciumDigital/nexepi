# Commandes Git Essentielles

## Initialiser un dépôt Git

```bash
git init
```

## Vérifier l'état du dépôt

```bash
git status
```

## Ajouter des fichiers au staging (index)

```bash
git add <fichier>        # Ajouter un fichier spécifique
git add .                # Ajouter tous les fichiers modifiés
```

## Faire un commit

```bash
git commit -m "Message du commit"
```

## Voir l'historique des commits

```bash
git log
```

## Ajouter un dépôt distant (ex: GitHub)

```bash
git remote add origin https://github.com/utilisateur/nom-du-repo.git
```

## Envoyer les commits sur le dépôt distant (push)

```bash
git push origin main     # Pour la branche main
git push origin master   # Pour la branche master
```

## Récupérer les dernières modifications du dépôt distant (pull)

```bash
git pull origin main
```

## Créer un tag (version)

```bash
git tag v1.0.0
```

## Lister les tags

```bash
git tag
```

## Pousser un tag vers le dépôt distant

```bash
git push origin v1.0.0
```

## Pousser tous les tags en une fois

```bash
git push --tags
```

## Cloner un dépôt existant

```bash
git clone https://github.com/utilisateur/nom-du-repo.git
```

## Changer de branche

```bash
git checkout nom-de-branche
```

## Créer et changer de branche en même temps

```bash
git checkout -b nouvelle-branche
```

---

Garde ce fichier à jour et tu auras toujours tes commandes Git importantes sous la main !
