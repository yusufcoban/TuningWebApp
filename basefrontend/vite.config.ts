import { fileURLToPath, URL } from 'node:url'
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { viteStaticCopy } from 'vite-plugin-static-copy'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        vue(),
        viteStaticCopy({
            targets: [
                {
                    src: 'src/assets/*',     // Use a wildcard to copy the contents of the folder
                    dest: 'src/assets'           // Destination directory in `dist` (no extra folder)
                }
            ]
        })
    ],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '/api': {
                target: 'https://localhost:4430', // Backend server URL
                changeOrigin: true,  // Needed to handle CORS
                secure: false,       // Allow self-signed certificates if necessary
                rewrite: (path) => path.replace(/^\/api/, '') // Optional, if your backend routes start without `/api`
            }
        }
    }
})
