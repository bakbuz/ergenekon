import { A } from '@solidjs/router';
import { marked } from 'marked';
import { useStore } from "../../store";
import Comments from "./Comments";

const ListingMeta = props => (
  <div class="listing-meta">
    <A href={`@${props.listing?.author.username}`}>
      <img src={props.listing?.author.image} alt="" />
    </A>

    <div class="info">
      <A href={`@${props.listing?.author.username}`} class="author">
        {props.listing?.author.username}
      </A>
      <span class="date">{new Date(props.listing?.createdAt).toDateString()}</span>
    </div>

    <Show when={props.canModify} fallback={<span />}>
      <span>
        <A
          href={`ilan-yayinla/${props.listing.id}`}
          class="btn btn-outline-secondary btn-sm"
        >
          <i class="ion-edit" /> Edit Listing
        </A>
        <button class="btn btn-outline-danger btn-sm" onClick={props.onDelete}>
          <i class="ion-trash-a" /> Delete Listing
        </button>
      </span>
    </Show>
  </div>
);

export default ({ id }) => {
  const [store, { deleteListing }] = useStore(),
    listing = () => store.listings[id],
    canModify = () =>
      store.currentUser && store.currentUser.username === listing()?.author.username,
    handleDeleteListing = () => deleteListing(id).then(() => (location.hash = "/"));

  return (
    <div class="listing-page">
      <div class="banner">
        <div class="container">
          <h1>{listing()?.title}</h1>
          <ListingMeta listing={listing()} canModify={canModify()} onDelete={handleDeleteListing} />
        </div>
      </div>

      <div class="container page">
        <div class="row listing-content">
          <div class="col-xs-12">
            <div innerHTML={listing() && marked.parse(listing()?.body, { sanitize: true })} />

            <ul class="tag-list">
              {listing()?.tagList.map(tag => (
                <li class="tag-default tag-pill tag-outline">{tag}</li>
              ))}
            </ul>
          </div>
        </div>

        <hr />

        <div class="listing-actions" />

        <div class="row">
          <Comments />
        </div>
      </div>
    </div>
  );
};
