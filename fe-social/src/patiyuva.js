export const initTheme = function () {
  const theme = getTheme();
  setTheme(theme);
}

export const getTheme = function () {
  let theme = localStorage.getItem("patiyuva.theme")
  if (theme == null || theme == "")
    theme = "light"

  return theme
}

export const currentThemeIsDark = function () {
  const theme = getTheme()
  return theme === "dark"
}

export const setTheme = function (theme) {
  localStorage.setItem("patiyuva.theme", theme);
  document.documentElement.setAttribute('data-bs-theme', theme)
  setModeSwitch(theme)
}

const setModeSwitch = function (theme) {
  document.querySelectorAll('button.btn-modeswitch').forEach(el => {
    if (el.getAttribute('data-theme') == theme)
      el.classList.add('active')
    else
      el.classList.remove('active')
  });
}

const setListingCreateLight = function () {
  const btn = document.getElementById('listing-create');
  btn.classList.remove('btn-dark');
  btn.classList.add('btn-primary');
}

const setListingCreateDark = function () {
  const btn = document.getElementById('listing-create');
  btn.classList.add('btn-dark');
  btn.classList.remove('btn-primary');
}