import React, { Component, ChangeEvent } from 'react'
import classNames from 'classnames'
import { Button, Card, Col, Form, InputGroup, ListGroup, Table } from 'react-bootstrap'
import { config } from '../Constants'
import { IProduct } from './ProductList'
import { ICategory } from './Categories'
import { X } from 'react-bootstrap-icons'
import { returnStatement } from '@babel/types'

export interface IContractorPrice {
	contractorId: number
	contractorExternalId: number
	contractorName: number
	productId: number
	price: number
}
interface IProductEditorState {
	product: IProduct,
	addingContractor: { nip?: number, price?: number }
	foundContractor?: { KontrahentId: number, Nazwa: string }
	contractorPrices: IContractorPrice[]
}

interface IProductEditorProps {
	productId?: number
	categories: ICategory[]
	onSubmit: () => void
	onCancel: () => void
}

class ProductEditor extends Component<IProductEditorProps, IProductEditorState> {
	private urlBase = config.url.API_URL
	private contractorUrlBase = config.url.CONTRACTOR_API_URL

	constructor(props: Readonly<IProductEditorProps>) {
		super(props)

		this.state = {
			product: {
				id: 0,
				categoryId: -1,
				name: '',
				price: 0.00,
				vat: 23,
				unit: '',
				pkwiuCode: '',
				gtuCode: ''
			},
			addingContractor: {},
			contractorPrices: []
		}

		this.handleInputChange = this.handleInputChange.bind(this)
		this.handleSubmit = this.handleSubmit.bind(this)
		this.handleRemove = this.handleRemove.bind(this)
		this.handleAddContractorPrice = this.handleAddContractorPrice.bind(this)
		this.handleContractorInputChange = this.handleContractorInputChange.bind(this)
		this.fetchContractor = this.fetchContractor.bind(this)
	}

	handleInputChange(event: any, number?: boolean) {
		const target = event.target;
		const value = target.type === 'checkbox' ? target.checked : target.value;
		const name = target.name;
	
		this.setState({
			product: { ...this.state.product, [name]: number ? parseFloat(value) : value } as IProduct
		})
	}

	handleContractorInputChange(event: any) {
		const target = event.target;
		let value = target.type === 'checkbox' ? target.checked : target.value;
		const name = target.name;

		if (name === 'nip' && value.length === 10) {
			this.fetchContractor(value)
		}

		if (name === 'price') {
			value = parseFloat(value)
		}
	
		this.setState({
			addingContractor: { ...this.state.addingContractor, [name]: value } as IProductEditorState['addingContractor']
		})
	}

	handleSubmit(event: any) {
		const product = this.state.product
		if (product.categoryId === -1) {
			product.categoryId = this.props.categories[0].id
		}
		fetch(this.urlBase+'/api/Product/' + (product.id || ''), {
			method: this.state.product.id ? 'PUT' : 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(product)
		})
			.then(res => {
				console.log(this.state.product)
				if (res.status !== 200) return
				this.props.onSubmit()
			})
	}

	handleRemove(id: number) {
		fetch(this.urlBase+`/api/Product/${id}`, {
			method: 'DELETE',
		})
			.then(() => {
			})
	}

	handleRemoveContractorPrice(contractorId: number) {
		fetch(this.urlBase+`/api/ContractorPrice/${contractorId}/${this.props.productId}`, {
			method: 'DELETE',
		})
			.then(() => {
				this.fetchContractorPrices()
			})
	}

	fetchProduct() {
		fetch(this.urlBase+`/api/Product/${this.props.productId}`)
			.then(res => {
				if (res.status !== 200) return
				res.json().then((product) => {
					this.setState({ product: {...product, categoryId: product.category?.id} })
				})
			})
	}

	fetchContractor(nip: string) {
		fetch(this.contractorUrlBase+`/kontrahenci/${nip}`)
			.then(res => {
				if (res.status !== 200) {
					this.setState({foundContractor: undefined})
					return
				}

				res.json().then((contractor) => {
					this.setState({foundContractor: contractor})
				})
			})
	}

	fetchContractorPrices() {
		fetch(this.urlBase+`/api/ContractorPrice/Product/${this.props.productId}`)
			.then(res => {
				if (res.status !== 200) return
				res.json().then((contractorPrices) => {
					this.setState({ contractorPrices })
				})
			})
	}

	handleAddContractorPrice() {
		if (this.state.foundContractor) {
			fetch(this.urlBase+'/api/ContractorPrice/', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({ productId: this.state.product.id, contractorId: this.state.foundContractor.KontrahentId, contractorName: this.state.foundContractor.Nazwa, price: this.state.addingContractor.price })
			})
				.then(res => {
					if (res.status !== 200) return
					this.fetchContractorPrices()
				})
		}
	}

	componentDidMount() {
		if (this.props.productId !== undefined) {
			this.fetchProduct()
			this.fetchContractorPrices()
		}
	}


	componentWillUnmount() {
	}

	render() {
		return (
			<Form>
				<Form.Row>
					<Form.Group as={Col} md={8} controlId="Name">
						<Form.Label>Name</Form.Label>
						<Form.Control placeholder="Item name"
							name="name"
            				value={this.state.product.name}
            				onChange={this.handleInputChange} />
					</Form.Group>

					<Form.Group as={Col} controlId="Price">
						<Form.Label>Price</Form.Label>
						<Form.Control type="number" step="0.01"
							name="price"
            				value={this.state.product.price}
            				onChange={(e) => this.handleInputChange(e, true)} />
					</Form.Group>

					<Form.Group as={Col} controlId="Vat">
						<Form.Label>VAT</Form.Label>
						<Form.Control type="number" step="1"
							name="vat"
            				value={this.state.product.vat}
            				onChange={this.handleInputChange} />
					</Form.Group>
				</Form.Row>

				<Form.Row>
					<Form.Group as={Col} controlId="Unit">
						<Form.Label>Unit</Form.Label>
						<Form.Control placeholder="Unit"
							name="unit"
            				value={this.state.product.unit}
            				onChange={this.handleInputChange} />
					</Form.Group>

					<Form.Group as={Col} controlId="PKWiU">
						<Form.Label>PKWiU</Form.Label>
						<Form.Control 
							name="pkwiuCode"
            				value={this.state.product.pkwiuCode}
            				onChange={this.handleInputChange} />
					</Form.Group>

					<Form.Group as={Col} controlId="GTU">
						<Form.Label>GTU</Form.Label>
						<Form.Control 
							name="gtuCode"
            				value={this.state.product.gtuCode}
            				onChange={this.handleInputChange} />
					</Form.Group>

					<Form.Group as={Col} controlId="Category">
						<Form.Label>Category</Form.Label>
						<Form.Control
							as="select"
							name="categoryId"
							value={this.state.product.categoryId}
							onChange={(e) => this.handleInputChange(e, true)}>
								{this.props.categories.map((category, index) => {
									return (
										<option key={category.id} value={category.id}>{category.name}</option>
									)
								})}
							</Form.Control>
					</Form.Group>
				</Form.Row>
				<Button variant="primary" type="button" onClick={this.handleSubmit}>Submit</Button>
				<Button variant="secondary" type="button" className="ml-2" onClick={this.props.onCancel}>Cancel</Button>
				{this.props.productId && 
					<React.Fragment>
						<Table className="mb-2 mt-4">
							<thead>
								<tr>
									<th>Id</th>
									<th>Contractor Name</th>
									<th>Price</th>
									<th></th>
								</tr>
							</thead>
							<tbody>
								{this.state.contractorPrices.map(p => {
									return (
										<tr key={p.contractorId}>
											<td>{p.contractorExternalId}</td>
											<td>{p.contractorName}</td>
											<td>{p.price.toFixed(2)}</td>
											<td><a href="#" className="text-danger" onClick={()=>this.handleRemoveContractorPrice(p.contractorId)}><X/></a></td>
										</tr>
									)
								})}
							</tbody>
						</Table>
						<Form.Row>
							<Form.Group as={Col} md={8} >
								<Form.Label>NIP</Form.Label>
								<Form.Control placeholder="NIP" type="number" 
									name="nip"
									max={9999999999}
									onChange={this.handleContractorInputChange} />
							</Form.Group>

							<Form.Group as={Col}>
								<Form.Label>Price</Form.Label>
								<Form.Control type="number" step="0.01"
									name="price"
									onChange={this.handleContractorInputChange} />
							</Form.Group>

						</Form.Row>
						<p>{this.state.foundContractor ? this.state.foundContractor.Nazwa : "Contractor not found"}</p>
						<Button variant="primary" type="button" disabled={!this.state.foundContractor || !this.state.addingContractor.price} onClick={this.handleAddContractorPrice} className="mb-2">Add Contractor Price</Button> <br></br>
					</React.Fragment>
				}
				
			</Form>
		)
	}
}
export default ProductEditor