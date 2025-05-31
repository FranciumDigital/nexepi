<template>
  <main id="Home-page">
    <section class="welcome">
      <h1>Bienvenue <strong>{{ name }}</strong> sur NEXEPI</h1>
      <p class="intro">
        Gérez facilement vos équipements de protection avec visibilité et sécurité.
      </p>
    </section>

    <section class="stats-grid">
      <div class="stat-card green">
        <h2>{{ stats.inStock }}</h2>
        <p>Valides</p>
      </div>
      <div class="stat-card yellow">
        <h2>{{ stats.toVerify }}</h2>
        <p>À vérifier</p>
      </div>
      <div class="stat-card red">
        <h2>{{ stats.damaged }}</h2>
        <p>Endommagés</p>
      </div>
      <div class="stat-card purple">
        <h2>{{ stats.expired }}</h2>
        <p>Périmés</p>
      </div>
      <div class="stat-card dark">
        <h2>{{ stats.rebuted }}</h2>
        <p>Rebutés</p>
      </div>
    </section>

    <section class="charts">
      <div class="chart-container">
        <h3>Répartition des statuts des EPI</h3>
        <canvas id="statusChart"></canvas>
        <p class="chart-desc">Les EPI en stock représentent actuellement la majorité du parc. 5 équipements sont à remplacer.</p>
      </div>
      <div class="chart-container">
        <h3>Évolution des ajouts mensuels</h3>
        <canvas id="monthlyAddChart"></canvas>
        <p class="chart-desc">Description</p>
      </div>
    </section>

    <section class="cta-buttons">
      <a href="/items/list" class="cta-button">Liste des EPI</a>
      <a href="/add" class="cta-button">Ajouter un EPI</a>
      <a href="/check" class="cta-button">Vérifier un EPI</a>
    </section>
  </main>
</template>

<style scoped>
#Home-page {
  padding: 2rem;
  width: 100%;
  margin: auto;
  font-family: 'Segoe UI', sans-serif;
}

.welcome {
  text-align: center;
  margin-bottom: 2rem;
}

.welcome h1 {
  font-size: 2.2rem;
  margin-bottom: 0.5rem;
}

.intro {
  font-size: 1.1rem;
  color: #555;
}

.stats-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(130px, 1fr));
  gap: 0.8rem;
  margin-bottom: 2rem;
}

.stat-card {
  padding: 0.7rem;
  text-align: center;
  border-radius: 6px;
  color: #fff;
  font-weight: bold;
}

.stat-card h2 {
  font-size: 1.3rem;
  margin: 0;
}

.stat-card p {
  font-size: 0.9rem;
  margin: 0.3rem 0 0 0;
}

.green { background-color: #4CAF50; }
.blue { background-color: #2196F3; }
.yellow { background-color: #FFC107; color: #000; }
.red { background-color: #F44336; }
.purple { background-color: #9C27B0; }
.dark { background-color: #333; }

.charts {
  display: grid;
  grid-template-columns: repeat(2, 1fr); /* ✅ Cette ligne aligne les 2 graphiques côte à côte */
  gap: 2rem;
  margin-bottom: 2rem;
}

.chart-container {
  background-color: #fff;
  padding-top: 1rem;
  padding-bottom: 5rem;
  border-radius: 8px;
  box-shadow: 0 1px 5px #00000010;
  max-height: 500px; /* facultatif : ajuste la hauteur si besoin */
}

.chart-container h3 {
  text-align: center;
  margin-bottom: 1rem;
  font-size: 1.2rem;
}

.chart-desc {
  text-align: center;
  font-size: 0.9rem;
  color: #666;
  margin-top: 0.5rem;
}

.cta-buttons {
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
  gap: 1rem;
}

.cta-button {
  padding: 10px 20px;
  background-color: var(--color-theme-primary);
  color: var(--color-theme-secondary);
  text-decoration: none;
  border-radius: 4px;
  transition: background-color 0.2s ease;
}

.cta-button:hover {
  background-color: var(--color-dark);
  color: var(--color-light);
}

</style>

<script>
import Chart from 'chart.js/auto';

export default {
  data() {
    return {
      name: 'John',
      stats: {
        total: 120,
        inStock: 80,
        outOfStock: 20,
        toVerify: 10,
        damaged: 5,
        expired: 5,
        rebuted: 3,
      }
    };
  },
  mounted() {
    this.initCharts();
  },
  methods: {
    initCharts() {
      const statusCtx = document.getElementById('statusChart');
      const monthlyCtx = document.getElementById('monthlyAddChart');

      new Chart(statusCtx, {
        type: 'doughnut',
        data: {
          labels: ['Valides', 'À vérifier', 'Cassés', 'Périmés', 'Rebutés'],
          datasets: [{
            data: [
              this.stats.inStock,
              this.stats.toVerify,
              this.stats.damaged,
              this.stats.expired,
              this.stats.rebuted
            ],
            backgroundColor: ['#4CAF50', '#FFC107', '#F44336', '#9C27B0', '#333']
          }]
        },
        options: {
          plugins: {
            legend: {
              position: 'bottom'
            }
          }
        }
      });

      new Chart(monthlyCtx, {
			type: 'bar',
			data: {
				labels: [
				'Jan', 'Fév', 'Mar', 'Avr', 'Mai', 'Juin', 
				'Juillet', 'Août', 'Sept', 'Oct', 'Nov', 'Déc'
				],
				datasets: [
				{
					label: 'En stock',
					data: [5, 12, 9, 14, 10, 11, 7, 8, 9, 10, 11, 12],
					backgroundColor: '#4CAF50'
				},
				{
					label: 'À vérifier',
					data: [2, 4, 3, 2, 3, 2, 4, 3, 5, 3, 4, 2],
					backgroundColor: '#FFC107'
				},
				{
					label: 'Périmés',
					data: [1, 2, 1, 1, 0, 1, 2, 1, 1, 2, 1, 1],
					backgroundColor: '#F44336'
				}
				]
			},
			options: {
				responsive: true,
				plugins: {
				title: {
					display: true,
					text: 'Évolution mensuelle par statut d\'EPI'
				},
				tooltip: {
					mode: 'index',
					intersect: false
				}
				},
				scales: {
				x: {
					stacked: true
				},
				y: {
					stacked: true,
					beginAtZero: true,
					title: {
					display: true,
					text: 'Nombre d\'EPI'
					}
				}
				}
			}
		});
    }
  }
};
</script>
