import { createComputed, createMemo, useTransition, lazy } from "solid-js";
import { useStore } from "../../store";
const Listings = lazy(() => import("./Listings"));

export default function() {
  const [store, { loadListings, setPage }] = useStore(),
    { token, appName } = store,
    tab = createMemo(() => {
      const search = location.search.split("?")[1];
      if (!search) return token ? "feed" : "all";
      const query = new URLSearchParams(search);
      return query.get("tab");
    }),
    [, start] = useTransition(),
    getPredicate = () => {
      switch (tab()) {
        case "feed":
          return { myFeed: true };
        case "all":
          return {};
        case undefined:
          return undefined;
        default:
          return { tag: tab() };
      }
    },
    handleSetPage = page => {
      start(() => {
        setPage(page);
        loadListings(getPredicate());
      });
    };

  createComputed(() => loadListings(getPredicate()));

  return Listings({ handleSetPage, appName, token, tab, store });
}
