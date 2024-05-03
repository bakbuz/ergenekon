import { A } from "@solidjs/router";
import { createStore } from "solid-js/store";
import ListErrors from "../../components/ListErrors";
import { useStore } from "../../store";

export default () => {
  const [state, setState] = createStore({ inProgress: false })
  const [, { register }] = useStore();

  const handleSubmit = e => {
    e.preventDefault();
    setState({ inProgress: true });
    register(state.username, state.email, state.password)
      .then(() => (location.hash = "/"))
      .catch(errors => setState({ errors }))
      .finally(() => setState({ inProgress: false }));
  };

  return (
    <main>
      <div class="container">
        <div class="row justify-content-center align-items-center vh-100 py-5">
          <div class="col-sm-10 col-md-8 col-lg-7 col-xl-6 col-xxl-5">
            <div class="card card-body rounded-3 p-4 p-sm-5">
              <div class="text-center">
                <h1 class="mb-2">Yeni bir hesap oluştur</h1>
                <span class="d-block">Zaten üye misin? Giriş yapmak için <A href="/giris">buraya tıklayın</A></span>
              </div>
              <ListErrors errors={state.errors} />
              <form class="mt-4" onSubmit={handleSubmit}>
                <div class="mb-3 input-group-lg">
                  <input type="text" class="form-control" placeholder="Kullanıcı adı" required
                    value={state.username}
                    onChange={e => setState({ username: e.target.value })} />
                </div>
                <div class="mb-3 input-group-lg">
                  <input type="email" class="form-control" placeholder="E-posta" required
                    value={state.email}
                    onChange={e => setState({ email: e.target.value })} />
                  <small class="form-text">E-postanızı asla başkalarıyla paylaşmayacağız.</small>
                </div>
                <div class="mb-3 position-relative">
                  <div class="input-group input-group-lg">
                    <input class="form-control fakepassword" type="password" id="psw-input" placeholder="Parola" required
                      value={state.password}
                      onChange={e => setState({ password: e.target.value })} />
                    <span class="input-group-text p-0">
                      <i class="fakepasswordicon bi bi-eye-slash cursor-pointer p-2 w-40px"></i>
                    </span>
                  </div>
                  <div id="pswmeter" class="mt-2"></div>
                  <div class="d-flex mt-1">
                    <div id="pswmeter-message" class="rounded"></div>
                    <div class="ms-auto">
                      <i class="bi bi-info-circle ps-1" data-bs-container="body" data-bs-toggle="popover" data-bs-placement="top" data-bs-content="Include at least one uppercase, one lowercase, one special character, one number and 8 characters long." data-bs-original-title="" title=""></i>
                    </div>
                  </div>
                </div>
                <div class="d-grid">
                  <small>Hesap oluştur düğmesine tıklayarak, <A href="javascript:;">Kullanım Koşulları</A>'nı ve <A href="javascript:;">Gizlilik Bildirimini</A>'ni kapsayan <A href="javascript:;">Çerez Kullanımı</A>'nı kabul etmiş olursun.</small>
                  <button type="submit" class="btn btn-lg btn-primary mt-3" disabled={state.inProgress}>Hesap oluştur</button>
                </div>
                <p class="mb-0 mt-3 text-center">&copy; 2023&nbsp;<a target="_blank" href="https://patiyuva.com">Patiyuva.</a> Tüm hakları saklıdır.</p>
              </form>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
};
