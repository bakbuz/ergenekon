import { createResource } from "solid-js";

export default function createComments(agent, actions, state, setState) {
  const [comments, { mutate, refetch }] = createResource(
    () => state.listingSlug,
    agent.Comments.forListing,
    { initialValue: [] }
  );
  Object.assign(actions, {
    loadComments(listingSlug, reload) {
      if (reload) return refetch()
      setState({ listingSlug });
    },
    async createComment(comment) {
      const { errors } = await agent.Comments.create(state.listingSlug, comment);
      if (errors) throw errors;
    },
    async deleteComment(id) {
      mutate(comments().filter((c) => c.id !== id));
      try {
        await agent.Comments.delete(state.listingSlug, id);
      } catch (err) {
        actions.loadComments(state.listingSlug);
        throw err;
      }
    }
  });
  return comments;
}
