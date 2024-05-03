import { createEffect, createResource } from "solid-js";

export default function createBreeds(agent, actions, state, setState) {

  const [breeds] = createResource("breeds",
    () => agent.Breeds.getAll(),
    { initialValue: [] }
  );
  return breeds;
}
