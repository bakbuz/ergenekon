import {  lazy } from "solid-js";
import { useStore } from "../../store";
const Home = lazy(() => import("./Home"));

export default function() {
  const [store] = useStore(),
    { token, appName } = store

  return Home({ appName, token, store });
}
