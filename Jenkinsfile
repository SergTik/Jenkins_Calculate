node{
checkout scm
stage("Fix the permission issue") 
sh "sudo chown root:jenkins /run/docker.sock"
stage "disable docker"
sh label: '', script: 'sudo docker stop calc || true'
sh label: '', script: 'sudo docker rm -v $(sudo docker ps -a -q -f status=exited) || true'
stage "restore"
sh label: '', script: 'dotnet restore -r linux-x64'
stage "Testing"
sh label: '', script: 'dotnet test'	
stage "build"
sh label: '', script: 'sudo docker build --no-cache -t image -f Dockerfile .'
stage "run"
sh label: '', script: 'sudo docker run -d -p 8000:80 --name calc image'       

}
