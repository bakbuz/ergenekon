import { createComputed, lazy } from "solid-js";
import { useStore } from "../../store";
const Profile = lazy(() => import("./Profile"));

export default function (props) {
  const [, { loadProfile, loadListings }] = useStore();
  createComputed(() => props.routeName === "profile" && loadProfile(props.params[0]));
  createComputed(
    () =>
    props.routeName === "profile" &&
      (location().includes("/favorites")
        ? loadListings({ favoritedBy: props.params[0] })
        : loadListings({ author: props.params[0] }))
  );
  return <Profile username={props.params[0]} />;
}
