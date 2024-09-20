import { Route, Router, Routes, useRoutes } from "@solidjs/router";
import { createComputed, createSignal, lazy } from "solid-js";
import { useStore } from "./store";

import Home from "./pages/Home";

import './assets/main.scss';
import './assets/style.css';

const Login = lazy(() => import("./pages/Auth/Login"));
const Register = lazy(() => import("./pages/Auth/Register"));
const PasswordRecovery = lazy(() => import("./pages/Auth/PasswordRecovery"));
const PasswordReset = lazy(() => import("./pages/Auth/PasswordReset"));

const Profile = lazy(() => import("./pages/Profile"));
const Settings = lazy(() => import("./pages/Settings"));

export default () => {
  const [store, { pullUser }] = useStore()
  const [appLoaded, setAppLoaded] = createSignal(false)

  if (!store.token) {
    setAppLoaded(true);
  }
  else {
    pullUser();
    createComputed(() => store.currentUser && setAppLoaded(true));
  }

  return (
    <>
      <Suspense fallback={<div class="container">Yükleniyor...</div>}>
        <Router>          

          <Show when={appLoaded()}>
            <Routes>
              <Route path="giris" component={Login} />
              <Route path="kaydol" component={Register} />
              <Route path="parola-kurtar" component={PasswordRecovery} />
              <Route path="parola-sifirla" component={PasswordReset} />

              <Route path="settings" component={Settings} />
              <Route path="u/:username" component={Profile} />

              <Route path="" component={Home} />
            </Routes>
          </Show>          

        </Router>
      </Suspense>
    </>
  );
};
