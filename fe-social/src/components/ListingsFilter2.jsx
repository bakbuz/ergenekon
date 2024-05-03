import { createSignal, For } from "solid-js";
import { useStore } from "../store";
import { useLocation, useNavigate, useSearchParams } from "@solidjs/router";

export default () => {
	const [searchParams, setSearchParams] = useSearchParams();
	const navigate = useNavigate();
	const location = useLocation();
	const [store, { }] = useStore();

	const allBreeds = store.breeds
	const [species, setSpecies] = createSignal([])
	const [breeds, setBreeds] = createSignal([])

	const [provinces, setProvinces] = createSignal([])
	const [districts, setDistricts] = createSignal([])
	const [neighborhoods, setNeighborhoods] = createSignal([])

	const [specieId, setSpecieId] = createSignal(searchParams.specie);
	const [breedId, setBreedId] = createSignal(searchParams.breed);
	const [provinceId, setProvinceId] = createSignal(searchParams.province);
	const [districtId, setDistrictId] = createSignal(searchParams.district);
	const [neighborhoodId, setNeighborhoodId] = createSignal(searchParams.neighborhood);
	const [searchterm, setSearchterm] = createSignal(searchParams.searchterm || "");

	// functions
	const loadSpecies = () => {
		const list = allBreeds.filter(f => f.parentId == 0);
		setSpecies(list)
	}

	const loadBreeds = () => {
		console.log("Breeds loading")
		if (!specieId()) {
			setSearchParams({ breed: "" })
			setBreedId("")
			setBreeds([])
			return
		}

		console.log("Breeds loading...")
		const id = parseInt(specieId())
		const list = allBreeds.filter(f => f.parentId == id);
		setBreeds(list)
		console.log("Breeds loaded")
	}

	// page load
	loadSpecies();
	loadBreeds();

	// event handlers
	const handleChange = (e) => new Promise((resolve, reject) => {
		e.preventDefault();
		const { name, value } = e.currentTarget;
		if (location.pathname == "/") {
			console.warn("routing to listings page")
			const target = `/ilanlar?${name}=${value}`
			navigate(target)
			resolve(value)
		}
		else {
			console.log(name, value)
			setSearchParams({ [name]: value })
			resolve(value)
		}
	});

	return (
		<div class="row row-cols-md-6 g-2 align-items-center patiyuva-listing-horizontal-filter">
			<div class="col-12">
				<label class="form-label">Tür</label>
				<select class="form-select" name="specie" value={specieId()} onChange={(e) => { handleChange(e).then((id) => { setSpecieId(id); loadBreeds() }) }}>
					<option value="">-- Tümü --</option>
					<For each={species()}>
						{(item) =>
							<option value={item.id} selected={item.id == specieId()}>{item.name}</option>
						}
					</For>
				</select>
			</div>

			<div class="col-12">
				<label class="form-label">Cins</label>
				<select class="form-select" name="breed" value={breedId()} onChange={handleChange} disabled={breeds().length === 0}>
					<option value="">-- Tümü --</option>
					<For each={breeds()}>
						{(item) =>
							<option value={item.id} selected={item.id == breedId()}>{item.name}</option>
						}
					</For>
				</select>
			</div>

			<div class="col-12">
				<label class="form-label">İl</label>
				<select class="form-select" name="province" value={provinceId()} onChange={(e) => { handleChange(e).then(id => { setProvinceId(id); loadDistricts() }) }}>
					<option value="">-- Tümü --</option>
					<For each={provinces()}>
						{(item) =>
							<option value={item.id} selected={item.id == provinceId()}>{item.name}</option>
						}
					</For>
				</select>
			</div>

		</div>
	);
};
