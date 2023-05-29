namespace RpgBooks.Libraries.Module.Infrastructure.Services.Dev;

/// <summary>
/// Email notification templates.
/// </summary>
internal static class DevTeamEmailNotificationConstants
{
    /// <summary>
    /// Gets exception email template.
    /// </summary>
    internal const string AdditionalMessageTemplate = @"
<div>
	<h2>Additional information</h2>
	<table>
		<tbody>
		    <tr>
			    <th>Message</th>
			    <td>{0}</td>
		    </tr>
		</tbody>
	</table>
</div>";

    /// <summary>
    /// Gets exception email template.
    /// </summary>
    internal const string ExceptionTemplate = @"
<div>
	<h2>Exception information</h2>
	<table>
		<tbody>
		    <tr>
			    <th>Message</th>
			    <td>{0}</td>
		    </tr>
		    <tr>
			    <th>Source</th>
			    <td>{1}</td>
		    </tr>
		    <tr>
			    <th>Type</th>
			    <td>{2}</td>
		    </tr>
		    <tr>
			    <th>Full name</th>
			    <td>{3}</td>
		    </tr>
		</tbody>
	</table>
	<hr>
	<h2>Stack trace</h2>
	<pre><code>{4}</code></pre>
</div>";

    /// <summary>
    /// Gets exception email template.
    /// </summary>
    internal const string AppDataTemplate = @"
<div>
	<h2>Application information</h2>
	<table>
		<tbody>
		    <tr>
			    <th>Application Name</th>
			    <td>{0}</td>
		    </tr>
		    <tr>
			    <th>Framework</th>
			    <td>{1}</td>
		    </tr>
		    <tr>
			    <th>Host OS</th>
			    <td>{2}</td>
		    </tr>
		    <tr>
			    <th>Base directory</th>
			    <td>{3}</td>
		    </tr>
		</tbody>
	</table>
</div>";

    /// <summary>
    /// Gets request email template.
    /// </summary>
    internal const string RequestTemplate = @"
<div>        
	<h2>Request information</h2>
	<table>
		<tbody>
			<tr>
				<th>Host</th>
				<td>{0}</td>
			</tr>
			<tr>
				<th>Path</th>
				<td>{1}</td>
			</tr>
			<tr>
				<th>Query string</th>
				<td>{2}</td>
			</tr>
			<tr>
				<th>Method</th>
				<td>{3}</td>
			</tr>
			<tr>
				<th>Content type</th>
				<td>{4}</td>
			</tr>
			<tr>
				<th>Content length</th>
				<td>{5}</td>
			</tr>
			<tr>
				<th>Protocol</th>
				<td>{6}</td>
			</tr>
			<tr>
				<th>Is HTTPs</th>
				<td>{7}</td>
			</tr>
			<tr>
				<th>Headers</th>
				<td>{8}</td>
			</tr>
			<tr>
				<th>Connection Id</th>
				<td>{9}</td>
			</tr>
			<tr>
				<th>Local address</th>
				<td>{10}</td>
			</tr>
			<tr>
				<th>Remote address</th>
				<td>{12}</td>
			</tr>
			<tr>
				<th>User</th>
				<td>{14}</td>
			</tr>
			<tr>
				<th>Claims</th>
				<td>{15}</td>
			</tr>
			<tr>
				<th>Request body</th>
				<td>{16}</td>
			</tr>
		</tbody>
	</table>
</div>";

    /// <summary>
    /// Gets body email template.
    /// </summary>
    internal const string BodyTemplate = @"
<!DOCTYPE html>
<html>
	<head>
        {0}
    </head>
	<body>
	<h2><b>Exception:</b> {1}</h2>
	<hr />
        {7}
    <hr />
	<div>
		{2}
	</div>
    <hr />
	<div>
		{3}
	</div>
	<hr />
	<div>
		{4}
	</div>
	<div class=""inner-exception"">
		<h2>Inner Exception: {5}</h2>
	  	<div>
			{6}
	  	</div>
	</div>
	</body>
</html>";

    /// <summary>
    /// Gets email template styles tag.
    /// </summary>
    internal const string CellRowTemplate = "<div style=\"margin: -.5rem 0; padding: .5rem 0; border-bottom: solid 1px hsl(0,0%,80%)\">{0} - {1}</div>";

    /// <summary>
    /// Gets email template styles tag.
    /// </summary>
    internal const string TemplateStyles = @"
<style>
    html{
		font-size: 16px;
		font-family: ""Google Sans"",Roboto,RobotoDraft,Helvetica,Arial,sans-serif;
	}

	h2, h3, h4, div, pre, p {
	  	padding: 0;
	  	margin:  0;
	}
	  	  
	pre {
		margin: 1rem 0;
	  	padding: 10px;
	  	overflow: auto;
	  	background: hsl(0,0%,96.5%);
	}

    hr {
		margin-bottom: 2rem;
	}
	  
	table{
	  	border-collapse: collapse;
	}
	  
	table th, table td{
        text-align: left;
	  	padding: .5rem;
	  	border: solid 1px hsl(0,0%,80%);
	}

	.inner-exception{
		padding-left: 1rem;
	}
</style>";
}
