import { createContext, useContext } from "solid-js";
import { createStore } from "solid-js/store";
import createAgent from "./createAgent";
import createListings from "./createListings";
import createAuth from "./createAuth";
import createCommon from "./createCommon";
import createBreeds from "./createBreeds";
import createCategories from "./createCategories";
import createComments from "./createComments";
import createProfile from "./createProfile";

const StoreContext = createContext();

export function Provider(props) {
  let listings, comments, tags, breeds,categories, profile, currentUser;
  const [state, setState] = createStore({
    get listings() {
      return listings();
    },
    get comments() {
      return comments();
    },
    get tags() {
      return tags();
    },
    get breeds() {
      return breeds();
    },
    get categories() {
      return categories();
    },
    get profile() {
      return profile();
    },
    get currentUser() {
      return currentUser();
    },
    page: 0,
    totalPagesCount: 0,
    token: localStorage.getItem("patiyuva.token"),
    appName: "patiyuva"
  }),
    actions = {},
    store = [state, actions],
    agent = createAgent(store);

  listings = createListings(agent, actions, state, setState);
  comments = createComments(agent, actions, state, setState);
  tags = createCommon(agent, actions, state, setState);
  breeds = createBreeds(agent, actions, state, setState);
  categories = createCategories(agent, actions, state, setState);
  profile = createProfile(agent, actions, state, setState);
  currentUser = createAuth(agent, actions, setState);

  return (
    <StoreContext.Provider value={store}>{props.children}</StoreContext.Provider>
  );
}

export function useStore() {
  return useContext(StoreContext);
}

