<template>
  <main id="Users-page">
    <h1 class="page-title">Utilisateurs</h1>

    <div class="table-container">
      <table class="user-table">
        <thead>
          <tr>
            <th>Nom</th>
            <th>Prénom</th>
            <th>Email</th>
            <th>Téléphone</th>
            <th>Rôle</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="user in users" :key="user.id">
            <td>{{ user.lastName }}</td>
            <td>{{ user.firstName }}</td>
            <td>{{ user.email }}</td>
            <td>{{ user.phone }}</td>
            <td>
              <span :class="['role-chip', user.role.toLowerCase()]">
                {{ user.role }}
              </span>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </main>
</template>

<script>
export default {
  name: 'UsersPage',
  data() {
    return {
      users: [
        {
          id: 1,
          firstName: 'Jean',
          lastName: 'Dupont',
          email: 'jean.dupont@example.com',
          phone: '06 12 34 56 78',
          role: 'Admin',
        },
        {
          id: 2,
          firstName: 'Claire',
          lastName: 'Morel',
          email: 'claire.morel@example.com',
          phone: '07 98 76 54 32',
          role: 'Inspecteur',
        },
        {
          id: 3,
          firstName: 'Luc',
          lastName: 'Martin',
          email: 'luc.martin@example.com',
          phone: '06 01 23 45 67',
          role: 'Lecteur',
        },
      ]
    }
  }
}
</script>

<style scoped>
#Users-page {
  padding: 2rem;
  font-family: 'Segoe UI', sans-serif;
  width: 100%;
}

.page-title {
  font-size: 2rem;
  margin-bottom: 1.5rem;
  color: var(--color-dark);
}

.table-container {
  overflow-x: auto;
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 6px;
  box-shadow: 0 2px 8px #00000015;
}

.user-table {
  width: 100%;
  border-collapse: collapse;
  min-width: 800px;
}

.user-table thead {
  background-color: var(--color-theme-primary);
  color: var(--color-theme-secondary);
}

.user-table th,
.user-table td {
  padding: 0.75rem 1rem;
  text-align: left;
  border-bottom: 1px solid #e0e0e0;
}

.user-table tbody tr:hover {
  background-color: #f9f9f9;
}

.role-chip {
  padding: 4px 10px;
  border-radius: 12px;
  font-size: 0.85rem;
  font-weight: 500;
  color: #fff;
  display: inline-block;
  text-transform: capitalize;
}

.role-chip.admin {
  background-color: #1976d2;
}

.role-chip.inspecteur {
  background-color: #ff9800;
}

.role-chip.lecteur {
  background-color: #9c27b0;
}
</style>
