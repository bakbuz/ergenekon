import { A, useNavigate } from "@solidjs/router";
import { Show } from "solid-js";
import logo from '../assets/img/patiyuva.svg';
import placeholder from '../assets/img/placeholder.jpg';
import { useStore } from "../store";

export default () => {
  const navigate = useNavigate();
  const [store, { logout }] = useStore();

  function changeTheme(theme) {
    setTheme(theme)
  }

  const handleLogout = e => {
    e.preventDefault()
    logout()
    //location.reload()
    navigate("/")
  }

  return (
    <header class="navbar-light fixed-top header-static bg-mode">
      <nav class="navbar navbar-expand-lg">
        <div class="container">

          <A class="navbar-brand" href="/" title={store.appName}>
            <div class="d-flex align-middle patiyuva-logo">
              <img src={logo} alt="Her pati bir yuva bekler" width="24" />
              <span class="ms-2 fs-4 d-none d-sm-inline">Patiyuva</span>
            </div>
          </A>

          <Show
            when={store.currentUser}
            fallback={(
              <ul class="nav flex-nowrap align-items-center ms-sm-3 list-unstyled">
                <li class="nav-item">
                  <A class="nav-link" href="/giris">Giriş</A>
                </li>
                <li class="nav-item">
                  <A class="nav-link" href="/kaydol">Kaydol</A>
                </li>
                <li class="nav-item">
                  <A class="btn btn-primary" href="/kaydol?ret=ilan-yayinla">
                    <i class="bi bi-journal-plus fs-6"></i>&nbsp;
                    <span class="d-none d-md-inline">Ücretsiz İlan Yayınla</span>
                  </A>
                </li>
              </ul>
            )}>

            <ul class="nav flex-nowrap align-items-center ms-sm-3 list-unstyled">

              {/* <li class="nav-item ms-2"><A class="nav-link icon-md btn btn-light p-0" href="my-account"><i class="bi bi-gear-fill fs-6"> </i></A></li> */}
              <li class="nav-item dropdown ms-2 notifications">
                <A class="nav-link btn btn-light icon-md p-0" href="javascript:;" id="notifDropdown" role="button" data-bs-toggle="dropdown" data-bs-auto-close="outside" aria-expanded="false">
                  <span class="badge-notif animation-blink"></span>
                  <i class="bi bi-bell-fill fs-6"></i>
                </A>
                <div class="dropdown-menu dropdown-animation dropdown-menu-end p-0 shadow-lg border-0" aria-labelledby="notifDropdown">
                  <div class="card">
                    <div class="card-header d-flex justify-content-between align-items-center">
                      <h6 class="m-0">Notifications</h6>

                      <div class="dropdown">
                        <button class="btn btn-sm btn-light" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                          <i class="bi bi-three-dots-vertical"></i>
                        </button>
                        <ul class="dropdown-menu dropdown-menu-end">
                          <li><A class="dropdown-item" href="javascript:;"><i class="bi bi-check-lg"></i> Tümünü okundu olarak işaretle</A></li>
                          <li><A class="dropdown-item" href="javascript:;"><i class="bi bi-gear"></i> Bildirim ayarları</A></li>
                        </ul>
                      </div>

                    </div>
                    <div class="card-body p-0">
                      <ul class="list-group list-group-flush list-unstyled p-2">

                      </ul>
                    </div>
                    <div class="card-footer text-center">
                      <A href="javascript:;" class="btn btn-sm btn-primary-soft">See all incoming activity</A>
                    </div>
                  </div>
                </div>
              </li>

              <li class="nav-item dropdown ms-2">
                <a class="nav-link btn icon-md p-0" href="javascript:;" id="profileDropdown" role="button" data-bs-toggle="dropdown" data-bs-auto-close="outside" data-bs-display="static" aria-expanded="false">
                  <img class="avatar-img rounded-2" src={store.currentUser.image || placeholder} alt={store.currentUser.username} />
                </a>
                <ul class="dropdown-menu dropdown-animation dropdown-menu-end pt-3 small me-md-n3" aria-labelledby="profileDropdown">
                  <li class="px-3">
                    <div class="d-flex align-items-center position-relative">
                      <div class="avatar me-3">
                        <img class="avatar-img rounded-circle" src={store.currentUser.image || placeholder} alt={store.currentUser.username} />
                      </div>
                      <div>
                        <A class="h6 stretched-link" href="javascript:;">{store.currentUser.displayName || store.currentUser.username}</A>
                        <p class="small m-0">Standart paket</p>
                      </div>
                    </div>
                    <A class="dropdown-item btn btn-primary-soft btn-sm my-2 text-center" href={`@${store.currentUser.username}`}>Herkese Açık Profil</A>
                  </li>
                  <li><A class="dropdown-item" href="my-account"><i class="bi bi-gear me-2"></i>Hesap bilgilerim</A></li>
                  <li><A class="dropdown-item" href="my-listings"><i class="bi bi-journal-text me-2"></i>İlanlarım</A></li>
                  <li><A class="dropdown-item" href="my-favorites"><i class="bi bi-star-fill me-2"></i>Favorilerim</A></li>
                  <li><A class="dropdown-item" href="my-orders"><i class="bi bi-credit-card me-2"></i>Siparişlerim</A></li>
                  <li class="dropdown-divider"></li>
                  <li><a class="dropdown-item bg-danger-soft-hover" href="#" onClick={handleLogout}><i class="bi bi-power me-2"></i>Çıkış Yap</a></li>
                  <li>
                    <hr class="dropdown-divider" />
                  </li>
                  <li>
                    <div class="modeswitch-item theme-icon-active d-flex justify-content-center gap-3 align-items-center p-2 pb-0">
                      <span>Tema:</span>
                      <button type="button" class="btn btn-modeswitch nav-link text-primary-hover" onClick={[changeTheme, 'light']} data-theme="light" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="Açık">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-sun mode-switch" viewBox="0 0 16 16">
                          <path d="M8 11a3 3 0 1 1 0-6 3 3 0 0 1 0 6zm0 1a4 4 0 1 0 0-8 4 4 0 0 0 0 8zM8 0a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-1 0v-2A.5.5 0 0 1 8 0zm0 13a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-1 0v-2A.5.5 0 0 1 8 13zm8-5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1 0-1h2a.5.5 0 0 1 .5.5zM3 8a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1 0-1h2A.5.5 0 0 1 3 8zm10.657-5.657a.5.5 0 0 1 0 .707l-1.414 1.415a.5.5 0 1 1-.707-.708l1.414-1.414a.5.5 0 0 1 .707 0zm-9.193 9.193a.5.5 0 0 1 0 .707L3.05 13.657a.5.5 0 0 1-.707-.707l1.414-1.414a.5.5 0 0 1 .707 0zm9.193 2.121a.5.5 0 0 1-.707 0l-1.414-1.414a.5.5 0 0 1 .707-.707l1.414 1.414a.5.5 0 0 1 0 .707zM4.464 4.465a.5.5 0 0 1-.707 0L2.343 3.05a.5.5 0 1 1 .707-.707l1.414 1.414a.5.5 0 0 1 0 .708z" />
                          <use href="javascript:;"></use>
                        </svg>
                      </button>
                      <button type="button" class="btn btn-modeswitch nav-link text-primary-hover" onClick={[changeTheme, 'dark']} data-theme="dark" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="Koyu">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-moon-stars mode-switch" viewBox="0 0 16 16">
                          <path d="M6 .278a.768.768 0 0 1 .08.858 7.208 7.208 0 0 0-.878 3.46c0 4.021 3.278 7.277 7.318 7.277.527 0 1.04-.055 1.533-.16a.787.787 0 0 1 .81.316.733.733 0 0 1-.031.893A8.349 8.349 0 0 1 8.344 16C3.734 16 0 12.286 0 7.71 0 4.266 2.114 1.312 5.124.06A.752.752 0 0 1 6 .278zM4.858 1.311A7.269 7.269 0 0 0 1.025 7.71c0 4.02 3.279 7.276 7.319 7.276a7.316 7.316 0 0 0 5.205-2.162c-.337.042-.68.063-1.029.063-4.61 0-8.343-3.714-8.343-8.29 0-1.167.242-2.278.681-3.286z" />
                          <path d="M10.794 3.148a.217.217 0 0 1 .412 0l.387 1.162c.173.518.579.924 1.097 1.097l1.162.387a.217.217 0 0 1 0 .412l-1.162.387a1.734 1.734 0 0 0-1.097 1.097l-.387 1.162a.217.217 0 0 1-.412 0l-.387-1.162A1.734 1.734 0 0 0 9.31 6.593l-1.162-.387a.217.217 0 0 1 0-.412l1.162-.387a1.734 1.734 0 0 0 1.097-1.097l.387-1.162zM13.863.099a.145.145 0 0 1 .274 0l.258.774c.115.346.386.617.732.732l.774.258a.145.145 0 0 1 0 .274l-.774.258a1.156 1.156 0 0 0-.732.732l-.258.774a.145.145 0 0 1-.274 0l-.258-.774a1.156 1.156 0 0 0-.732-.732l-.774-.258a.145.145 0 0 1 0-.274l.774-.258c.346-.115.617-.386.732-.732L13.863.1z" />
                          <use href="javascript:;"></use>
                        </svg>
                      </button>
                      <button type="button" class="btn btn-modeswitch nav-link text-primary-hover" onClick={[changeTheme, 'auto']} data-theme="auto" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="Sistem">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-circle-half mode-switch" viewBox="0 0 16 16">
                          <path d="M8 15A7 7 0 1 0 8 1v14zm0 1A8 8 0 1 1 8 0a8 8 0 0 1 0 16z" />
                          <use href="javascript:;"></use>
                        </svg>
                      </button>
                    </div>
                  </li>
                </ul>
              </li>


              <li class="nav-item ms-2">
                <A class="btn btn-primary" href="/ilan-yayinla">
                  <i class="bi bi-journal-plus fs-6"></i>&nbsp;
                  <span class="d-none d-md-inline">Ücretsiz İlan Yayınla</span>
                </A>
              </li>

            </ul>
          </Show>



        </div>
      </nav>
    </header>
  );
};

{/*         
<li class="nav-item">
              <A class="nav-link" route="editor">
                <i class="ion-compose" /> New Post
              </NavLink>
            </li>
            <li class="nav-item">
              <A class="nav-link" route="settings">
                <i class="ion-gear-a" /> Settings
              </NavLink>
            </li>
  */}