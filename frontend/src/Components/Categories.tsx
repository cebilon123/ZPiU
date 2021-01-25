import React, { Component, ChangeEvent } from 'react'
import classNames from 'classnames'
import { Button, Card, Form, InputGroup, ListGroup } from 'react-bootstrap'
import { config } from '../Constants'
import { X } from 'react-bootstrap-icons'

export interface ICategory {
	id: number
	name: string
}

interface ICategoriesState {
	categories: ICategory[]
}

interface ICategoriesProps {
	onCategoriesChanged: (categories: ICategory[]) => void
}

class Categories extends Component<ICategoriesProps, ICategoriesState> {
	private urlBase = config.url.API_URL
	private fieldValue = ''

	constructor(props: Readonly<ICategoriesProps>) {
		super(props)

		this.state = {
			categories: [],
		}

		this.handleChange = this.handleChange.bind(this)
		this.handleAdd = this.handleAdd.bind(this)
		this.handleRemove = this.handleRemove.bind(this)
	}
	
	handleChange(event: any) {
		this.fieldValue = event.target.value
	}

	handleAdd(event: any) {
		fetch(this.urlBase+'/api/Category', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ Name: this.fieldValue })
		})
			.then(res => res.json())
			.then(() => {
				this.fetchAll()
			})
	}

	handleRemove(id: number) {
		fetch(this.urlBase+`/api/Category/${id}`, {
			method: 'DELETE',
		})
			.then(res => res.json())
			.then(() => {
				this.fetchAll()
			})
	}

	fetchAll() {
		fetch(this.urlBase+'/api/Category/List')
			.then(res => res.json())
			.then((categories) => {
				this.setState({ categories })
				this.props.onCategoriesChanged(categories)
			})
	}

	componentDidMount() {
		this.fetchAll()
	}


	componentWillUnmount() {
	}

	render() {
		return (
			<Card>
				<Card.Header>Categories</Card.Header>
				<ListGroup variant="flush">
					{this.state.categories.map((value, index) => {
        				return (
							<ListGroup.Item key={index}>{value.name} <a href="#" className="text-danger float-right" onClick={()=>this.handleRemove(value.id)}><X/></a></ListGroup.Item>
						)
						})
					}
				
					<ListGroup.Item>
						<InputGroup>
							<Form.Control type="text" placeholder="New Category" onChange={this.handleChange}/>
							<InputGroup.Append>
								<Button onClick={this.handleAdd}>Add</Button>
							</InputGroup.Append>
						</InputGroup>
					</ListGroup.Item>
					
				</ListGroup>
			</Card>
		)
	}
}
export default Categories