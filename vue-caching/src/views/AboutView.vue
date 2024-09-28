<script lang="ts">
import { defineComponent } from 'vue'
import { useQuery } from '@tanstack/vue-query'
import axios from 'axios'

interface Post {
  userId: number
  id: number
  title: string
  body: string
}

const fetcher = async (): Promise<Post[]> =>
  await fetch('https://jsonplaceholder.typicode.com/posts').then((response) => response.json())
export default defineComponent({
  name: 'PostsList',
  setup() {
    const { isPending, isError, isFetching, data, error, refetch } = useQuery({
      queryKey: ['posts'],
      queryFn: fetcher,
      gcTime: 300000000, // Lưu trữ dữ liệu trong cache trong 5 phút (tính bằng mili giây)
      staleTime: 60000000,
      refetchOnWindowFocus: false
    })

    return { isPending, isError, isFetching, data, error, refetch }
  }
})
</script>

<template>
  qeopjruiwoerheiwqr
  <h1>Posts</h1>
  <div v-if="isPending">Loading...</div>
  <div v-else-if="isError">An error has occurred: {{ error }}</div>
  <div v-else-if="data">
    <ul>
      <li v-for="item in data" :key="item.id">
        {{ item.title }}
      </li>
    </ul>
  </div>
</template>

<style scoped>
.visited {
  font-weight: bold;
  color: green;
}
</style>
