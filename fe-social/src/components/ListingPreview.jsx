import { A } from "@solidjs/router";
import placeholder from '../assets/img/placeholder.jpg';
import { prettyDate } from '../utils'

const FAVORITED_CLASS = "btn btn-sm btn-primary";
const NOT_FAVORITED_CLASS = "btn btn-sm btn-outline-primary";

export default ({ listing, token, onClickFavorite }) => {
  let image = listing.image;
  if (!image) {
    image = placeholder
  }

  return (
    <div class="col">
      <div class="card p-0 shadow-none border-0 position-relative">
        <div class="position-relative">
          <A href={`/ilan/${listing.slug}-${listing.id}`}>
            <img class="w-100" src={image} alt="" />
          </A>
          <small class="date">{prettyDate(listing.createdAt)}</small>
          <p><i class="bi bi-geo-alt"></i> Maltepe, İstanbul</p>
        </div>
        <div class="card-body px-0">
          <div class="d-flex align-items-center mb-3">
            <div class="avatar avatar-xxs me-2">
              <img class="avatar-img rounded-circle" src={listing.author.image || placeholder} alt="" />
            </div>
            <p class="mb-0">
              <A href={`/@${listing.author.username}`} class="inactive">{listing.author.username}</A>
            </p>
          </div>
          <h6 class="mb-0">
            <A href={`/ilan/${listing.slug}-${listing.id}`}>{listing.title}</A>
          </h6>
          <div class="listing-meta">
            {token && (
              <div class="pull-xs-right">
                <button
                  class={listing.favorited ? FAVORITED_CLASS : NOT_FAVORITED_CLASS}
                  onClick={[onClickFavorite, listing]}
                >
                  <i class="bi bi-heart"></i> {listing.favoritesCount}
                </button>
              </div>
            )}
          </div>

        </div>
      </div>
    </div>
  );
};
