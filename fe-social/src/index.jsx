/* @refresh reload */
import { render } from 'solid-js/web';
import App from './App';
import { initTheme } from './patiyuva';
import { Provider } from './store';

render(() => (
  <Provider>
    <App />
  </Provider>
), document.body);

initTheme();