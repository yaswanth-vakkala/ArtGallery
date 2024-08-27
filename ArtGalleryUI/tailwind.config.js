/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}",],
  theme: {
    extend: {},
  },
  plugins: [
    require('daisyui'),
  ],
  // prefix: "tw-",
  // corePlugins: {
  //     preflight: false,
  // }
}

