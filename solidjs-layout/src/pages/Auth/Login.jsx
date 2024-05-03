import { A, useNavigate } from "@solidjs/router";
import { createStore } from "solid-js/store";
import ListErrors from "../../components/ListErrors";
import { useStore } from "../../store";

export default () => {
  const navigate = useNavigate();
  const [state, setState] = createStore({ email: 'bayram@maydere.com', password: 'ASdf12,,', inProgress: false });
  const [, { login }] = useStore();

  const handleSubmit = e => {
    e.preventDefault();
    setState({ inProgress: true });
    login(state.email, state.password)
      .then(() => navigate("/"))
      .catch(errors => setState({ errors }))
      .finally(() => setState({ inProgress: false }));
  };

  return (
    <main>
      <div class="container">
        <div class="row justify-content-center align-items-center vh-100 py-5">
          <div class="col-sm-10 col-md-8 col-lg-7 col-xl-6 col-xxl-5">
            <div class="card card-body text-center p-4 p-sm-5">
              <h1 class="mb-2">Giriş yap</h1>
              <p class="mb-0">Hesabın yok mu? Hemen <A href="/kaydol">kaydol</A></p>
              <ListErrors errors={state.errors} />
              <form class="mt-sm-4" onSubmit={handleSubmit}>
                <div class="mb-3 input-group-lg">
                  <input type="email" class="form-control" placeholder="E-posta" required
                    value={state.email}
                    onChange={e => setState({ email: e.target.value })} />
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
                </div>
                <div class="mb-3 d-sm-flex justify-content-end">
                  <A href="/parola-kurtar" tabindex="-1">Parolanı mı unuttun?</A>
                </div>
                <div class="d-grid"><button type="submit" class="btn btn-lg btn-primary" disabled={state.inProgress}>Giriş yap</button></div>
                <p class="mb-0 mt-3">&copy; 2023 &nbsp;
                  <a target="_blank" href="https://patiyuva.com">Patiyuva.</a> Tüm hakları saklıdır.
                </p>
              </form>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
};
