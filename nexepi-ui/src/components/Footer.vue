<template>
  <div class="greetings">
    <h3>
      <span>© 2025 NEXEPI par Francium Digital. Tous droits réservés.</span>
      <span>Contact : <a style="font-weight: bold;" href="mailto:letrangerobin@gmail.com">letrangerobin@gmail.com</a></span>
    </h3>
  </div>
</template>

<style scoped>
.greetings {
  width: 100%;
  background: var(--color-light);
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  padding: 10px;
}

h3 {
  font-size: 1rem;
  text-align: center;
  color: var(--color-dark);
}

span {
  display: block;
}

a {
  text-decoration: none;
  color: var(--color-dark);
}

a:hover {
  background-color: inherit;
  color: var(--color-theme-primary);
}
</style>
