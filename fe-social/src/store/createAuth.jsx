import { createSignal, createResource, batch } from "solid-js";

export default function createAuth(agent, actions, setState) {
  const [loggedIn, setLoggedIn] = createSignal(false),
    [currentUser, { mutate }] = createResource(loggedIn, agent.Auth.current);
  Object.assign(actions, {
    pullUser: () => setLoggedIn(true),
    async login1(email, password) {
      const { user, errors } = await agent.Auth.login(email, password);
      if (errors) throw errors;
      actions.setToken(user.accessToken);
      setLoggedIn(true);
    },
    async login(email, password) {
      agent.Auth.login(email, password)
        .then(user => {
          console.log(user)
          actions.setToken(user.accessToken);
          setLoggedIn(true);
        })
        .catch(err => {
          console.log(err)
          throw err.errors;
        })
    },
    async register(username, email, password) {
      const { user, errors } = await agent.Auth.register(username, email, password);
      if (errors) throw errors;
      actions.setToken(user.accessToken);
      setLoggedIn(true);
    },
    logout() {
      batch(() => {
        setState({ token: undefined });
        mutate(undefined);
      })
    },
    async updateUser(newUser) {
      const { user, errors } = await agent.Auth.save(newUser);
      if (errors) throw errors;
      mutate(user);
    }
  });
  return currentUser;
}
