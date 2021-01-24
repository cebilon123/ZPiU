import React, { Component, ChangeEvent } from 'react'
import classNames from 'classnames'
import { Button, Card, Col, Form, InputGroup, ListGroup, Table } from 'react-bootstrap'
import { config } from '../Constants'
import ProductEditor from './ProductEditor'
import { SortAlphaDown, SortAlphaUp, SortNumericDown, SortNumericUp } from 'react-bootstrap-icons';
import { ICategory } from './Categories'

export interface IProduct {
	id: number
	categoryId?: number
	category?: { id: number, name: string }
	name: string
	price: number
	vat: number
	unit: string
	pkwiuCode: string
	gtuCode: string
}

interface ISearchParams { id?: number, name?: string, categoryId?: number }

interface IProductListState {
	products: IProduct[]
	isAdding: boolean
	editingId?: number
	sortingBy: 'id' | 'name' | 'price'
	sortingOrder: 'asc' | 'desc'
	searchOpen: boolean
	searchParams?: ISearchParams
}

interface IProductListProps {
	categories: ICategory[]
}

class ProductList extends Component<IProductListProps, IProductListState> {
	private urlBase = config.url.API_URL

	constructor(props: Readonly<IProductListProps>) {
		super(props)

		this.state = {
			products: [],
			isAdding: false,
			sortingBy: 'id',
			sortingOrder: 'asc',
			searchOpen: false
		}

		this.handleAdd = this.handleAdd.bind(this)
		this.handleRemove = this.handleRemove.bind(this)
		this.handleSubmit = this.handleSubmit.bind(this)
		this.handleCancel = this.handleCancel.bind(this)
		this.handleEdit = this.handleEdit.bind(this)
		this.toggleSearch = this.toggleSearch.bind(this)
		this.handleSearchInputChange = this.handleSearchInputChange.bind(this)
	}

	handleAdd(event: any) {
		this.setState({
			isAdding: true,
			editingId: undefined,
		})
	}

	handleEdit(id: number) {
		this.setState({
			isAdding: false,
			editingId: id
		})
	}
	
	handleSubmit() {
		this.fetchAll()
		this.setState({
			isAdding: false,
			editingId: undefined
		})
	}

	handleCancel() {
		this.fetchAll()
		this.setState({
			isAdding: false,
			editingId: undefined
		})
	}

	handleRemove(id: number) {
		fetch(this.urlBase+`/api/Product/${id}`, {
			method: 'DELETE',
		})
			.then(() => {
				this.fetchAll()
			})
	}

	toggleSearch() {
		this.setState({
			searchOpen: !this.state.searchOpen,
			searchParams: this.state.searchOpen ? undefined : this.state.searchParams
		})
	}
	
	handleSearchInputChange(event: any, numeric?: boolean) {
		const target = event.target
		const value = target.type === 'checkbox' ? target.checked : numeric ? target.value === "" ? undefined : Number.parseInt(target.value) : target.value 
		const name = target.name
	
		this.setState({
			searchParams: { ...this.state.searchParams, [name]: value } as ISearchParams
		})
	}

	fetchAll() {
		fetch(this.urlBase+'/api/Product/')
			.then(res => {
				if (res.status !== 200) return
				res.json().then((products) => {
					this.setState({ products: products })
				})
			})
	}

	componentDidMount() {
		this.fetchAll()
	}


	componentWillUnmount() {
	}

	renderSortIcon(property: IProductListState['sortingBy'], numeric: boolean) {
		let order: IProductListState['sortingOrder'] = 'asc'
		let className: string | undefined = 'text-muted'

		const sort = () => {
			this.setState({
				sortingBy: property,
				sortingOrder: this.state.sortingBy === property ? this.state.sortingOrder === 'asc' ? 'desc' : 'asc' : this.state.sortingOrder
			})
		}

		if (this.state.sortingBy === property) {
			order = this.state.sortingOrder
			className = 'text-primary'
		}
		const props = { className, onClick: sort }
		if (numeric) {
			if (order === 'desc') {
				return <SortNumericUp {...props}/>
			} else {
				return <SortNumericDown {...props}/>
			}
		} else {
			if (order === 'desc') {
				return <SortAlphaUp {...props}/>
			} else {
				return <SortAlphaDown {...props}/>
			}
		}
	}

	renderSearch() {
		return (
			<Card.Body>
				<Form>
					<Form.Row>
						<Form.Group as={Col} controlId="Id">
							<Form.Label>Id</Form.Label>
							<Form.Control type="number"
								name="id"
								onChange={(e) => this.handleSearchInputChange(e, true)} />
						</Form.Group>
						<Form.Group as={Col} md={8} controlId="Name">
							<Form.Label>Name</Form.Label>
							<Form.Control
								name="name"
								onChange={this.handleSearchInputChange} />
						</Form.Group>

						<Form.Group as={Col} controlId="Category">
							<Form.Label>Category</Form.Label>
							<Form.Control
								as="select"
								name="categoryId"
								onChange={(e) => this.handleSearchInputChange(e, true)}>
									<option>All</option>
									{this.props.categories.map((category, index) => {
										return (
											<option key={category.id} value={category.id}>{category.name}</option>
										)
									})}
								</Form.Control>
						</Form.Group>
					</Form.Row>
				</Form>
			</Card.Body>
		)
	}

	render() {
		return (
			<Card>
				<Card.Header>Items <Button className="ml-4" onClick={this.handleAdd}>Add</Button> <Button variant="secondary" onClick={this.toggleSearch}>Search</Button></Card.Header>
				{this.state.searchOpen && this.renderSearch()}
				<Table striped hover>
					<thead>
						<tr>
							<th># {this.renderSortIcon('id', true)}</th>
							<th>Name {this.renderSortIcon('name', false)}</th> {/*style={{width: "100%"}}*/}
							<th>Price {this.renderSortIcon('price', true)}</th>
							<th>Unit</th>
							<th>VAT</th>
							<th>PKWiU</th>
							<th>GTU</th>
							<th>Category</th>
							<th></th>
						</tr>
					</thead>
					<tbody>
						{this.state.isAdding &&
							<tr><td colSpan={9}><ProductEditor onSubmit={this.handleSubmit} onCancel={this.handleCancel} categories={this.props.categories}></ProductEditor></td></tr>
						}
						{this.state.products.sort((a, b) => {
							if (this.state.sortingOrder === 'desc') [a, b] = [b, a]
							return typeof a[this.state.sortingBy] === 'string' ? (a[this.state.sortingBy] as string).localeCompare(b[this.state.sortingBy] as string) : (a[this.state.sortingBy] as number) - (b[this.state.sortingBy] as number)
						}).filter(p => {
							if (!this.state.searchParams) return true
							const { id, name, categoryId } = this.state.searchParams
							if (id !== undefined && id !== p.id) return false
							if (name && !p.name.toLocaleLowerCase().startsWith(name.toLocaleLowerCase())) return false
							if (categoryId !== undefined && !isNaN(categoryId) && p.category?.id !== categoryId) return false
							return true
						}).map((product, index) => {
							return (
								<React.Fragment>
								 	<tr key={product.id}>
										<td>{product.id}</td>
										<td>{product.name}</td>
										<td>{product.price.toFixed(2)}</td>
										<td>{product.unit}</td>
										<td>{product.vat}%</td>
										<td>{product.pkwiuCode}</td>
										<td>{product.gtuCode}</td>
										<td>{product.category?.name}</td>
										<td>
											<a href="#" className="text-primary" onClick={()=>this.handleEdit(product.id)}>Edit</a>&nbsp;&nbsp;<a href="#" className="text-danger" onClick={()=>this.handleRemove(product.id)}>Remove</a>
										</td>
									</tr>
									{this.state.editingId === product.id && <tr><td colSpan={9}><ProductEditor onSubmit={this.handleSubmit} onCancel={this.handleCancel} productId={product.id} categories={this.props.categories}></ProductEditor></td></tr>}
								</React.Fragment>)
							})
						}
					</tbody>
				</Table>
			</Card>
		)
	}
}
export default ProductList