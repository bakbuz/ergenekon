//const API_ROOT = "https://localhost:1923/api";
const API_ROOT = "http://localhost:1919/api";

export default function createAgent([state, actions]) {
  async function send(method, url, data, resKey) {
    const headers = {},
      opts = { method, headers };

    // headers['Access-Control-Allow-Origin'] = 'http://localhost:4200';
    // headers['Access-Control-Allow-Credentials'] = true;

    if (data !== undefined) {
      headers["Content-Type"] = "application/json";
      opts.body = JSON.stringify(data);
    }

    if (state.token)
      headers["Authorization"] = `Bearer ${state.token}`;

    try {
      const response = await fetch(API_ROOT + url, opts);
      const json = await response.json();
      return resKey ? json[resKey] : json;
    } catch (err) {
      if (err && err.response && err.response.status === 401) {
        actions.logout();
      }
      return err;
    }
  }

  const Auth = {
    current: () => send("get", "/user", undefined, "user"),
    login: (email, password) => send("post", "/auth/login", { email, password }),
    register: (username, email, password) => send("post", "/auth/register", { username, email, password }),
    save: user => send("put", "/user", { user })
  };

  const Profile = {
    follow: username => send("post", `/profiles/${username}/follow`),
    get: username => send("get", `/profiles/${username}`, undefined, "profile"),
    unfollow: username => send("delete", `/profiles/${username}/follow`)
  };

  return {
    Auth,
    Profile,
  };
}
