<template>
  <div class="app">
    <Sidebar class="sidebar" />

    <div class="main">
      <div class="header_class">
        <Header @show-warning="showWarning"/>
      </div>

      <div class="warning_class">
        <Warning v-show="isWarningVisible" @close-warning="isWarningVisible = false"/>
      </div>

      <div class="router_class">
        <router-view />
      </div>

      <div class="footer_class">
        <Footer/>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import Header from './components/Header.vue';
import Footer from './components/Footer.vue';
import Sidebar from './components/Sidebar.vue';
import Warning from './views/Warning.vue';

const isWarningVisible = ref(localStorage.getItem('isLoggedIn'));
function showWarning() {
  isWarningVisible.value = true;
}
</script>

<style lang="scss">
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: 'Fira Sans', sans-serif;
}

.app {
  display: flex;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  margin: 0;
  padding: 0;
}

.sidebar {
  height: 100vh;
}

.main {
  display: flex;
  flex-direction: column;
  flex: 1;
  height: 100vh;
  padding: 0;
  overflow-y: auto;
}

.header_class {
  display: flex;
  align-items: center;
  justify-content: center;
}

.router_class {
  flex: 1;
  margin: 0;
  display: flex;
}

.footer_class {
  display: flex;
  align-items: center;
  justify-content: center;
  margin-top: auto;
  text-align: center;
}
</style>
