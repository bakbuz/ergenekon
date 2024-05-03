import { lazy } from "solid-js";
import { useStore } from "../../store";
const Editor = lazy(() => import("./Editor"));

export default function(props) {
  const [, { loadListing }] = useStore(),
    slug = props.params[0];
  slug && loadListing(slug);
  return Editor({ slug });
}