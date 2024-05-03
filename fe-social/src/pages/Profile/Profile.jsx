import { useStore } from "../../store";
import ListingList from "../../components/ListingList";
import { A, useLocation, useParams } from "@solidjs/router";

export default props => {
  const location = useLocation()
  const username = location.pathname.substring(1, location.pathname.length).replace('/favorites', '')
  console.log(username)

  const [store, { setPage, loadListings, unfollow, follow }] = useStore()
  const handleClick = ev => {
    ev.preventDefault();
    store.profile.following ? unfollow() : follow();
  }
  const handleSetPage = page => {
    setPage(page);
    loadListings();
  }
  const isUser = () => store.currentUser && username === store.currentUser.username;

  return (
    <div class="profile-page">
      <div class="user-info">
        <div class="container">
          <div class="row">
            <div class="col-xs-12 col-md-10 offset-md-1">
              <img src={store.profile?.image} class="user-img" alt="" />
              <h4 textContent={username} />
              <p>{store.profile?.bio}</p>
              {isUser() && (
                <A href="/settings" class="btn btn-sm btn-outline-secondary action-btn">
                  <i class="ion-gear-a" /> Edit Profile Settings
                </A>
              )}
              {store.token && !isUser() && (
                <button
                  class="btn btn-sm action-btn"
                  classList={{
                    "btn-secondary": store.profile?.following,
                    "btn-outline-secondary": !store.profile?.following
                  }}
                  onClick={handleClick}
                >
                  <i class="ion-plus-round" /> {store.profile?.following ? "Unfollow" : "Follow"}{" "}
                  {store.profile?.username}
                </button>
              )}
            </div>
          </div>
        </div>
      </div>

      <div class="container">
        <div class="row">
          <div class="col-xs-12 col-md-10 offset-md-1">
            <div class="listings-toggle">
              <ul class="nav nav-pills outline-active">
                <li class="nav-item">
                  <A
                    class="nav-link"
                    active={!location.pathname.includes("/favorites")}
                    href={`/${username}`}
                  >
                    My Listings
                  </A>
                </li>

                <li class="nav-item">
                  <A
                    class="nav-link"
                    active={location.pathname.includes("/favorites")}
                    href={`/${username}/favorites`}
                  >
                    Favorited Listings
                  </A>
                </li>
              </ul>
            </div>

            <ListingList
              listings={Object.values(store.listings)}
              totalPagesCount={store.totalPagesCount}
              onSetPage={handleSetPage}
            />
          </div>
        </div>
      </div>
    </div>
  );
};
