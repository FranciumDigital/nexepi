<template>
  <main id="Settings-page">
    <h1>⚙️ Paramètres</h1>

    <nav class="tabs">
      <button
        v-for="tab in tabs"
        :key="tab"
        :class="{ active: activeTab === tab }"
        @click="activeTab = tab"
      >
        {{ tab }}
      </button>
    </nav>

    <section v-if="activeTab === 'Interface'" class="tab-content">
      <h2>🎨 Interface</h2>
      <label class="switch">
        <input type="checkbox" />
        <span class="slider"></span>
        Activer le mode sombre
      </label>
    </section>

    <section v-if="activeTab === 'Utilisateur'" class="tab-content">
      <h2>👤 Informations Utilisateur</h2>
      <form>
        <label>Nom d'utilisateur</label>
        <input type="text" placeholder="Votre nom d'utilisateur" />

        <label>Email</label>
        <input type="email" placeholder="email@exemple.com" />

        <label>Mot de passe</label>
        <input type="password" placeholder="********" />

        <label>Rôle</label>
        <select>
          <option>Administrateur</option>
          <option>Inspecteur</option>
          <option>Lecteur</option>
          <option>Utilisateur</option>
        </select>

        <button type="submit" class="cta-button">Valider les modifications</button>
      </form>
    </section>

    <section v-if="activeTab === 'Notifications'" class="tab-content">
      <h2>🔔 Notifications</h2>
      <label class="checkbox-label">
        <input type="checkbox" />
        Activer les notifications par email
      </label>
      <button class="cta-button">Valider</button>
    </section>

    <section v-if="activeTab === 'Entreprise'" class="tab-content">
      <h2>🏢 Paramètres de l’entreprise</h2>
      <form>
        <label>Nom de l’entreprise</label>
        <input type="text" placeholder="Nom de votre entreprise" />

        <label>Email de contact</label>
        <input type="email" placeholder="contact@entreprise.com" />

        <label>Couleur principale</label>
        <input type="color" />

        <label>Couleur secondaire</label>
        <input type="color" />

        <label>Logo</label>
        <input type="file" />

        <button type="submit" class="cta-button">Valider</button>
      </form>
    </section>

    <section v-if="activeTab === 'EPI'" class="tab-content">
      <h2>🧤 Paramètres EPI</h2>
      <button class="cta-button">Valider les paramètres EPI</button>
    </section>
  </main>
</template>

<script>
export default {
  name: "SettingsPage",
  data() {
    return {
      tabs: ["Interface", "Utilisateur", "Notifications", "Entreprise", "EPI"],
      activeTab: "Interface",
    };
  },
};
</script>

<style scoped>
#Settings-page {
 width: 100%;
  margin: 2rem auto;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  color: #333;
  padding: 0 1rem;
}

h1 {
  text-align: center;
  margin-bottom: 2rem;
  font-weight: 700;
  font-size: 2.5rem;
  color: #2c3e50;
}

.tabs {
  display: flex;
  justify-content: center;
  gap: 1rem;
  margin-bottom: 2rem;
}

.tabs button {
  background-color: #ecf0f1;
  border: none;
  padding: 10px 25px;
  border-radius: 25px;
  cursor: pointer;
  font-weight: 600;
  color: #34495e;
  transition: background-color 0.3s ease, color 0.3s ease;
}

.tabs button.active,
.tabs button:hover {
  background-color: #2980b9;
  color: white;
}

.tab-content {
  background: #f7f9fb;
  padding: 2rem;
  margin-bottom: 2rem;
  border-radius: 12px;
  box-shadow: 0 4px 10px rgb(0 0 0 / 0.05);
}

h2 {
  font-weight: 600;
  margin-bottom: 1rem;
  color: #2980b9;
}

form label {
  display: block;
  margin-bottom: 0.4rem;
  font-weight: 600;
  font-size: 0.95rem;
  color: #34495e;
}

input[type="text"],
input[type="email"],
input[type="password"],
select,
input[type="color"],
input[type="file"] {
  width: 100%;
  padding: 10px 12px;
  margin-bottom: 1.4rem;
  border: 1.5px solid #bdc3c7;
  border-radius: 8px;
  font-size: 1rem;
  transition: border-color 0.3s ease;
}

input[type="text"]:focus,
input[type="email"]:focus,
input[type="password"]:focus,
select:focus,
input[type="color"]:focus {
  border-color: #2980b9;
  outline: none;
}

.cta-button {
  display: inline-block;
  background-color: #2980b9;
  color: white;
  border: none;
  padding: 12px 30px;
  border-radius: 30px;
  cursor: pointer;
  font-weight: 700;
  font-size: 1.1rem;
  transition: background-color 0.3s ease;
  margin-top: 1rem;
}

.cta-button:hover {
  background-color: #1f6391;
}

.checkbox-label,
.switch {
  display: flex;
  align-items: center;
  font-weight: 600;
  font-size: 1rem;
  color: #34495e;
  margin-bottom: 1rem;
  gap: 10px;
}

/* Simple toggle switch style */
.switch input {
  width: 0;
  height: 0;
  opacity: 0;
  position: absolute;
}

.switch {
  position: relative;
  display: inline-block;
  width: 50px;
  height: 28px;
}

.switch .slider {
  position: absolute;
  cursor: pointer;
  top: 0; left: 0; right: 0; bottom: 0;
  background-color: #ccc;
  border-radius: 34px;
  transition: 0.4s;
}

.switch .slider:before {
  position: absolute;
  content: "";
  height: 22px;
  width: 22px;
  left: 3px;
  bottom: 3px;
  background-color: white;
  border-radius: 50%;
  transition: 0.4s;
}

.switch input:checked + .slider {
  background-color: #2980b9;
}

.switch input:checked + .slider:before {
  transform: translateX(22px);
}
</style>
