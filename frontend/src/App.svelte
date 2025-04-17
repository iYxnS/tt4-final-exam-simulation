<script>
	import { onMount } from 'svelte';

	let tasks = [];
	let newTitle = '';
	let newDescription = '';

	const loadTasks = async () => {
		const res = await fetch('http://localhost:5000/api/tasks');
		tasks = await res.json();
	};

	const createTask = async () => {
		await fetch('http://localhost:5000/api/tasks', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify({
				title: newTitle,
				description: newDescription,
				completed: false
			})
		});
		newTitle = '';
		newDescription = '';
		loadTasks();
	};

	const toggleComplete = async (task) => {
		await fetch(`http://localhost:5000/api/tasks/${task.id}`, {
			method: 'PUT',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify({ ...task, completed: !task.completed })
		});
		loadTasks();
	};

	const deleteTask = async (id) => {
		await fetch(`http://localhost:5000/api/tasks/${id}`, {
			method: 'DELETE'
		});
		loadTasks();
	};

	onMount(() => {
		loadTasks();
	});
</script>

<h1>Task Manager</h1>

<form on:submit|preventDefault={createTask}>
	<input
		bind:value={newTitle}
		type="text"
		placeholder="Task title"
		required
	/>
	<input
		bind:value={newDescription}
		type="text"
		placeholder="Description"
	/>
	<button type="submit">Add Task</button>
</form>

<ul>
	{#each tasks as task}
		<li>
			<input type="checkbox" bind:checked={task.completed} on:change={() => toggleComplete(task)} />
			<strong>{task.title}</strong>
			<em style="margin-left: 8px; font-size: 0.9em;">{task.description}</em>
			<button on:click={() => deleteTask(task.id)}>🗑</button>
		</li>
	{/each}
</ul>

<style>
	h1 {
		font-family: sans-serif;
		margin-bottom: 1rem;
	}
	form {
		margin-bottom: 1rem;
		display: flex;
		gap: 0.5rem;
	}
	input[type="text"] {
		padding: 0.5rem;
		font-size: 1rem;
	}
	button {
		padding: 0.5rem 1rem;
		font-size: 1rem;
		cursor: pointer;
	}
	ul {
		list-style: none;
		padding: 0;
	}
	li {
		margin: 10px 0;
		display: flex;
		align-items: center;
		gap: 0.5rem;
	}
</style>
