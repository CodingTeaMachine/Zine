/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./**/*.{razor,html}'],
  theme: {
    extend: {
      aspectRatio: {
        'comic-cover': '663/1024'
      }
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
  ],
}
