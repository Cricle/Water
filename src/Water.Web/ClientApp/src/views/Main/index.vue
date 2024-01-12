<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import Menu from "./Menu/index.vue";
</script>

<template>
  <div class="main">
    <header><Menu></Menu></header>
    <main class="content">
      <RouterView></RouterView>
    </main>
  </div>
</template>

<style lang="scss" scoped>
.main {
  height: 100%;
  .content {
    height: calc(100% - 84px);
    padding: 12px;
  }
}
</style>
