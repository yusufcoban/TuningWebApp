import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
    },
    server: {
        proxy: {
            '/api': {
                target: 'https://localhost:7257', // Backend server URL
                changeOrigin: true,  // Needed to handle CORS
                secure: false,       // Allow self-signed certificates if necessary
                rewrite: (path) => path.replace(/^\/api/, '') // Optional, if your backend routes start without `/api`
            }
        }
    }
})
