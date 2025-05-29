// src/functions/theme.js
import axios from 'axios'

export async function applyThemeColors() {
  const user_token = localStorage.getItem('token')

  if (user_token === 'valid-token') {
    const company_token = localStorage.getItem('company_token')
    console.log(`Company token : ${company_token}`)

    try {
      const response = await axios.get(
        `${import.meta.env.VITE_NEXEPI_API_URL}/settings/company?company_token=${company_token}`
      )

      const theme = response.data.data

      document.documentElement.style.setProperty('--color-theme-primary', theme.theme_primary_color)
      document.documentElement.style.setProperty('--color-theme-secondary', theme.theme_secondary_color)
    } catch (error) {
      console.error('Erreur lors de la récupération du thème :', error)
    }
  } else {
    // Valeurs par défaut
    document.documentElement.style.setProperty('--color-theme-primary', 'var(--vt-c-theme-primary-color)')
    document.documentElement.style.setProperty('--color-theme-secondary', 'var(--vt-c-theme-secondary-color)')
  }
}
