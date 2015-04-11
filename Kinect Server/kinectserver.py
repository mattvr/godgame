import cherrypy

cherrypy.config.update({'server.socket_host':'0.0.0.0','server.socket_port':1234})

class Root:
    exposed = True
    def GET(self):
        if 'message' in dir(self):
            return self.message
        else:
            return 'zilch'
    def POST(self, message):
        self.message = message
        return 'success'

cherrypy.quickstart(Root(), config={'/':{'request.dispatch':cherrypy.dispatch.MethodDispatcher()}})
