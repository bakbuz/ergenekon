import { createEffect, createResource } from "solid-js";

export default function createCategories(agent, actions, state, setState) {

  const [categories] = createResource("categories",
    () => agent.Categories.getAll(),
    { initialValue: [] }
  );
  return categories;
}
