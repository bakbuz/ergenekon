import { useParams } from "@solidjs/router";
import { lazy } from "solid-js";
import { useStore } from "../../store";
const Listing = lazy(() => import("./Listing"));

export default function () {
  const params = useParams()
  const [, { loadListing, loadComments }] = useStore()
  loadListing(params.id);
  loadComments(params.id);
  return Listing({ id: params.id });
}
