export function getRandomInt(min, max) {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

export function prettyDate(date) {
  return new Date(date).toDateString()
}