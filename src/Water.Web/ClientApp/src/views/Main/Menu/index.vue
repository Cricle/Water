<script setup lang="ts">
import { ref } from "vue";
interface Menu {
  key: string | number;
  label: string;
  icon?: string;
  path: string;
  position?:'right'|'left'
}
const props = withDefaults(
  defineProps<{
    menuList?: Menu[];
  }>(),
  {
    menuList: () => [
      {
        key: "0",
        label: "首页",
        icon: "House",
        path: "/",
      },
      {
        key: "1",
        label: "活动中",
        icon: "ChatLineSquare",
        path: "/Activity",
      },
      {
        key: "2",
        label: "历史",
        icon: "Document",
        path: "/History",
      },
      {
        key: "3",
        label: "个人信息",
        icon: "User",
        path: "/User",
        position:'right'
      },
      {
        key: "4",
        label: "",
        icon: "SwitchButton",
        path: "/Login",
        // position:'right'
      },
    ],
  }
);
const activeIndex = ref("/");

</script>
<template>
  <el-menu
    :default-active="activeIndex"
    router
    class="el-menu-demo"
    mode="horizontal"
    :ellipsis="false"
  >
    <template v-for="(item, index) in menuList">
      <template v-if="item.position === 'right'">
        <div class="flex-grow" ></div>
        <el-menu-item :key="item.key" :index="item.path">
          <el-icon><component :title="item.label" :is="item.icon" /></el-icon>
          <span>{{ item.label }}</span>
        </el-menu-item>
      </template>
      <template v-else>
        <el-menu-item :key="item.key" :index="item.path">
          <el-icon><component :is="item.icon" /></el-icon>
          <span>{{ item.label }}</span>
        </el-menu-item>
      </template>
    </template>
  </el-menu>
</template>
<style scoped>
.flex-grow {
  flex-grow: 1;
}
</style>
