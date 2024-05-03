import { createContext, useContext } from "solid-js";
import { createStore } from "solid-js/store";
import createAgent from "./createAgent";
import createAuth from "./createAuth";
import createProfile from "./createProfile";

const StoreContext = createContext();

export function Provider(props) {
  let profile, currentUser;
  const [state, setState] = createStore({
    get profile() {
      return profile();
    },
    get currentUser() {
      return currentUser();
    },
    page: 0,
    totalPagesCount: 0,
    token: localStorage.getItem("ergenekon.token"),
    appName: "ergenekon"
  }),
    actions = {},
    store = [state, actions],
    agent = createAgent(store);

  profile = createProfile(agent, actions, state, setState);
  currentUser = createAuth(agent, actions, setState);

  return (
    <StoreContext.Provider value={store}>{props.children}</StoreContext.Provider>
  );
}

export function useStore() {
  return useContext(StoreContext);
}

