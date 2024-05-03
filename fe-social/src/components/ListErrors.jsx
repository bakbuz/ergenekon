export default props => (
  <Show when={props.errors}>
    <ul class="alert alert-danger text-start mt-2">
      <For each={Object.keys(props.errors)}>
        {key => (
          <li>{props.errors[key].message}</li>
        )}
      </For>
    </ul>
  </Show>
);