
![]({{site.baseurl}}/HRON.png)
# HRON

The application was build with the need of having a central database for all employee data, manage HR masterdata, design and start onboarding workflows as well as monitor the assigned Tasks

The solution consists of 5 parts:
- HRON 					The main Application, written as a WPF application with [materialdesign toolkit](https://github.com/ButchersBoy/MaterialDesignInXamlToolkit)
- HRONLib 				The Database Layer, with the Database logic, entity framework code first files etc.
- HRONWeb				Some Webpage for Approving pending Tasks
- HRONWorkflowService	A WCF Project that hosts the main WF4.5 Workflows
- [RehostedDesigner](https://github.com/orosandrei/Rehosted-Workflow-Designer/tree/master/RehostedDesigner)		A Project that permits designing the WF4.5 Workflows directly from the HRON main programm.

