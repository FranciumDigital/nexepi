<template>
  <main id="About-page">
    <section class="hero">
      <h1>À propos de NEXEPI</h1>
      <p class="subheading">
        Plateforme intuitive de gestion des Équipements de Protection Individuelle (EPI)
      </p>
    </section>

    <section class="about-section">
      <h2>Fonctionnalités principales</h2>
      <ul class="features-list">
        <li>
          <strong>Accueil :</strong> Vue d’ensemble avec statistiques et graphiques dynamiques.
        </li>
        <li>
          <strong>Liste :</strong> Affiche tous les EPI enregistrés, avec accès aux détails et options de modification.
        </li>
        <li>
          <strong>Ajouter :</strong> Permet d’enregistrer un nouvel EPI en remplissant un formulaire détaillé.
        </li>
        <li>
          <strong>Vérifier :</strong> Liste des EPI à contrôler, avec formulaire de vérification.
        </li>
        <li>
          <strong>À propos :</strong> Cette page d’information sur la plateforme.
        </li>
        <li>
          <strong>Paramètres :</strong> Réglages personnalisés de l’application.
        </li>
        <li>
          <strong>Compte :</strong> Gestion du profil utilisateur, connexion et rôles.
        </li>
      </ul>
    </section>

    <section class="about-section">
      <h2>Notre mission</h2>
      <p>
        NEXEPI a été conçu pour simplifier la gestion des équipements de protection individuelle,
        garantir la conformité aux normes de sécurité, et faciliter le suivi du cycle de vie des équipements.
        Grâce à une interface ergonomique et des outils puissants, vous assurez la sécurité de vos équipes avec efficacité.
      </p>
    </section>
  </main>
</template>

<style scoped>
#About-page {
  padding: 2rem;
  font-family: 'Segoe UI', sans-serif;
  width: 95%;
  margin: auto;
}

.hero {
  text-align: center;
  margin-bottom: 2.5rem;
}

.hero h1 {
  font-size: 2.5rem;
  margin-bottom: 0.5rem;
  color: var(--color-theme-primary);
}

.subheading {
  font-size: 1.2rem;
  color: #555;
}

.about-section {
  margin-bottom: 2rem;
}

.about-section h2 {
  font-size: 1.6rem;
  color: var(--color-dark);
  margin-bottom: 1rem;
  border-left: 4px solid var(--color-theme-primary);
  padding-left: 10px;
}

.features-list {
  list-style: none;
  padding-left: 0;
}

.features-list li {
  padding: 0.6rem 0;
  border-bottom: 1px solid #eee;
  font-size: 1.05rem;
}

.features-list li {
  color: var(--color-theme-primary);
}

.about-section p {
  font-size: 1.1rem;
  line-height: 1.7;
  color: #444;
}
</style>
