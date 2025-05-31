<template>
	<aside :class="`${is_expanded ? 'is-expanded' : ''}`">

		<div class="menu-button">
			<button class="button" @click="ToggleMenu">
				<span class="material-icons">menu</span>
			</button>
		</div>

		<h3>Menu</h3>
		<div class="menu">
			<router-link to="/" class="buttons">
				<span class="material-icons">home</span>
				<span class="text">Acceuil</span>
			</router-link>
			<router-link to="/items/list" class="buttons">
				<span class="material-icons">list_alt</span>
				<span class="text">Liste des EPI</span>
			</router-link>
			<router-link to="/items/add" class="buttons">
				<span class="material-icons">add</span>
				<span class="text">Ajouter un EPI</span>
			</router-link>
			<router-link to="/users" class="buttons">
				<span class="material-icons">groups</span>
				<span class="text">utilisateurs</span>
			</router-link>
			<router-link to="/about" class="buttons">
				<span class="material-icons">help</span>
				<span class="text">A propos</span>
			</router-link>
		</div>

		<div class="flex"></div>
		
		<div class="menu">
			<router-link to="/settings" class="buttons">
				<span class="material-icons">settings</span>
				<span class="text">Paramètres</span>
			</router-link>
		</div>
	</aside>
</template>

<script setup>
import { ref } from 'vue'

const is_expanded = ref(localStorage.getItem("is_expanded") === "true")

const ToggleMenu = () => {
	is_expanded.value = !is_expanded.value
	localStorage.setItem("is_expanded", is_expanded.value)
}
</script>

<style lang="scss" scoped>
aside {
	display: flex;
	flex-direction: column;

	background-color: var(--color-dark);

	width: 4rem;
	overflow: hidden;
	min-height: 100vh;
	padding: 1rem;

	transition: 0.2s ease-in-out;

	.flex {
		flex: 1 1 0%;
	}

	h3, .buttons .text {
		opacity: 0;
		transition: opacity 0.3s ease-in-out;
	}

	h3 {
		font-size: 0.875rem;
		margin-bottom: 0.5rem;
		text-transform: uppercase;
		color: var(--color-light);
	}

	.menu {
		margin: 0 -1rem;
		

		.buttons {
			display: flex;
			align-items: center;
			text-decoration: none;

			transition: 0.2s ease-in-out;
			padding: 0.5rem 1rem;

			color: var(--color-light);

			.material-icons {
				font-size: 2rem;
				transition: 0.2s ease-in-out;
			}
			.text {
				transition: 0.2s ease-in-out;
			}

			&:hover {
				color: var(--color-theme-primary);
				background-color: var(--color-light);
			}

			&.router-link-exact-active {
				color: var(--color-theme-primary);
				background-color: var(--color-light);
				border-right: 10px solid var(--color-theme-primary);
			}
		}
	}

	.menu-button {
		margin: 0 -1rem;

		.button {
			display: flex;
			align-items: center;
			text-decoration: none;
			padding: 0.5rem 1rem;
			color: var(--color-background-soft);
			
			cursor: pointer;
			border: none;
			outline: none;
			background: none;
			
			.material-icons {
				font-size: 2rem;
			}
			
			.material-icons:hover {
				color: var(--color-background);
				padding: 0.125rem 0.125rem;
				font-size: 1.75rem;
			}
		}
	}

	.footer {
		opacity: 0;
		transition: opacity 0.3s ease-in-out;
	}

	&.is-expanded {
		width: 200px;

		h3, .buttons .text {
			opacity: 1;
		}

		.buttons {
			.material-icons {
				margin-right: 1rem;
			}
		}

		.footer {
			opacity: 0;
		}
	}

	@media (max-width: 10px) {
		position: absolute;
		z-index: 99;
	}
}
</style>