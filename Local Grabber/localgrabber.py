import cherrypy
from threading import Thread
import urllib
import time

cherrypy.config.update({'server.socket_host':'0.0.0.0','server.socket_port':1234})
globals()['HOMIE'] = ''

class Root:
    exposed = True
    def GET(self):
        return globals()['HOMIE']

def updateHomie():
    while True:
        try:
            globals()['HOMIE'] = urllib.urlopen("http://169.254.233.111:1234").read()
        except:
            pass

        time.sleep(1/50.)

Thread(target = updateHomie).start()

cherrypy.quickstart(Root(), config={'/':{'request.dispatch':cherrypy.dispatch.MethodDispatcher()}})
