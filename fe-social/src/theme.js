export const initTheme = function () {
  const theme = getTheme();
  setTheme(theme);
}

export const getTheme = function () {
  let theme = localStorage.getItem("ergenekon.theme")
  if (theme == null || theme == "")
    theme = "light"

  return theme
}

export const currentThemeIsDark = function () {
  const theme = getTheme()
  return theme === "dark"
}

export const setTheme = function (theme) {
  localStorage.setItem("ergenekon.theme", theme);
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

