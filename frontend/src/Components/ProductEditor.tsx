import React, { Component, ChangeEvent } from 'react'
import classNames from 'classnames'
import { Button, Card, Col, Form, InputGroup, ListGroup, Table } from 'react-bootstrap'
import { config } from '../Constants'
import { IProduct } from './ProductList'
import { ICategory } from './Categories'

interface IProductEditorState {
	product: IProduct
}

interface IProductEditorProps {
	productId?: number
	categories: ICategory[]
	onSubmit: () => void
	onCancel: () => void
}

class ProductEditor extends Component<IProductEditorProps, IProductEditorState> {
	private urlBase = config.url.API_URL

	constructor(props: Readonly<IProductEditorProps>) {
		super(props)

		this.state = {
			product: {
				id: 0,
				categoryId: 9,
				name: '',
				price: 0.00,
				vat: 23,
				unit: '',
				pkwiuCode: '',
				gtuCode: ''
			}
		}

		this.handleInputChange = this.handleInputChange.bind(this)
		this.handleSubmit = this.handleSubmit.bind(this)
		this.handleRemove = this.handleRemove.bind(this)
	}

	handleInputChange(event: any) {
		const target = event.target;
		const value = target.type === 'checkbox' ? target.checked : target.value;
		const name = target.name;
	
		this.setState({
		  product: { ...this.state.product, [name]: value } as IProduct
		})
	}

	handleSubmit(event: any) {
		const product = this.state.product
		//delete product['category']
		fetch(this.urlBase+'/api/Product/' + (this.state.product.id || ''), {
			method: this.state.product.id ? 'PUT' : 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(product)
		})
			.then(res => {
				console.log(this.state.product)
				if (res.status !== 200) return
				res.json().then((product) => {
					this.props.onSubmit()
				})
			})
	}

	handleRemove(id: number) {
		fetch(this.urlBase+`/api/Product/${id}`, {
			method: 'DELETE',
		})
			.then(res => res.json())
			.then(() => {
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

	handleAddContractorPrice() {

	}

	componentDidMount() {
		if (this.props.productId !== undefined) {
			this.fetchProduct()
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
            				onChange={this.handleInputChange} />
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
							onChange={this.handleInputChange}>
								{this.props.categories.map((category, index) => {
									return (
										<option value={category.id}>{category.name}</option>
									)
								})}
							</Form.Control>
					</Form.Group>
				</Form.Row>
				<Table striped className="mb-2">
					<thead>
						<tr>
							<th>#</th>
							<th>Contractor</th>
							<th>Price</th>
							<th></th>
						</tr>
					</thead>
					<tbody>
						
					</tbody>
				</Table>
				
				<Button variant="primary" type="button" onClick={this.handleSubmit} className="mb-2">Add Contractor Price</Button> <br></br>

				<Button variant="primary" type="button" onClick={this.handleSubmit}>Submit</Button>
				<Button variant="secondary" type="button" className="ml-2" onClick={this.props.onCancel}>Cancel</Button>
			</Form>
		)
	}
}
export default ProductEditor