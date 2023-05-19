import Splitting from 'https://cdn.skypack.dev/splitting'
import ScrollOut from 'https://cdn.skypack.dev/scroll-out'

const randomInRange = (min, max) =>
	Math.floor(
		Math.random() * (Math.floor(max) - Math.ceil(min) + 1) + Math.ceil(min)
	)

// Used the randomize the order of the panels. Remove for development.
const PANEL_CONTAINER = document.querySelector('ul')
const PANELS = [...document.querySelectorAll('li')].sort((a, b) => {
	if (
		a.innerText === 'Scroll for cascading text effects!' ||
		b.innerText === 'Scroll for cascading text effects!' ||
		a.innerText === "Thanks for scrollin'!" ||
		b.innerText === "Thanks for scrollin'!"
	) {
		return 0
	}
	return Math.random() > 0.5 ? -1 : 1
})
PANEL_CONTAINER.innerHTML = ''
PANELS.forEach((panel) => PANEL_CONTAINER.appendChild(panel))
