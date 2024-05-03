import { A } from "@solidjs/router";

export default ({ listing }) => {
	let cssClass = 'rounded w-100';
	if (listing.super === true) {
		cssClass += ' border border-5 border-info'
	}
	return (
		<div class="col">
			<div class="card p-0 shadow-none border-0 position-relative">
				<div class="position-relative">
					<A href="/listing-details-2">
						<img class={cssClass} src={listing.image} alt="" />
					</A>
					<div class="position-absolute bottom-0 start-0 p-2 d-flex w-100">
						<span class="bg-dark bg-opacity-50 px-2 rounded text-white small">erkek</span>
						<span class="bg-dark bg-opacity-50 px-2 rounded text-white small ms-auto">2 aylık</span>
					</div>
				</div>
				<div class="card-body px-0">
					<h6 class="mb-0">
						<A class="stretched-link" href="/listing-details-2">{listing.name}</A>
					</h6>
				</div>
			</div>
		</div>
	);
};
