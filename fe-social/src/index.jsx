/* @refresh reload */
import { render } from 'solid-js/web';
import App from './App';
import { initTheme } from './theme';
import { Provider } from './store';

render(() => (
  <Provider>
    <App />
  </Provider>
), document.body);

initTheme();