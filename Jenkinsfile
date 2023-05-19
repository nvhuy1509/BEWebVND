pipeline {
    agent any

    options {
        buildDiscarder(logRotator(numToKeepStr: '3'))
        disableConcurrentBuilds()
        timeout(time: 15, unit: 'MINUTES')
        timestamps()
    }

    environment {
        def BUILD_DATE = sh(script: "echo `date +%F`", returnStdout: true).trim()
        CONFIG_APPSETTINGS = "myG.CMS.School/appsettings.json"
        AWS_ACCOUNT_ID = "072515712417"
        AWS_DEFAULT_REGION = "ap-southeast-1"
        AWS_CREDS = credentials('aws_consangtao_deploy_ecs')
        AWS_ECS_TASK_DEFINITION = "taskDefinition.json"
        // Không cần phân biệt repository uri, chỉ cần tạo 1 lần cho cả dev,prod 
        //  => Bước build image đặt tag phân biệt dev và prod => Sửa containerDefinitions ở config aws_task_definition
        REPOSITORY_URI = '072515712417.dkr.ecr.ap-southeast-1.amazonaws.com/gameguild'
        SERVICE_NAME = 'gameguild'
        // cluster
        DEV_CLUSTER_NAME = 'dev-gameguild'
        PROD_CLUSTER_NAME = 'prod-gameguild'
        // Taskname vẫn phải sửa bằng tay ở Jekinsfile và ở config prod_aws_task_definition
    }



    stages {

        stage('Check tools required') {
            steps {
		cleanWs()
                sh '''
                docker --version
                aws --version
                '''
            }
        }


        stage('Logging into AWS ECR') {
            steps {
                script {
                    sh "aws ecr get-login-password --region ${AWS_DEFAULT_REGION} | docker login --username AWS --password-stdin ${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_DEFAULT_REGION}.amazonaws.com"
                }
            }
        }


        stage('Load Task Definition') {
            parallel {
                stage('Develop') {
                    when {
                        expression {
                            BRANCH_NAME ==~ '(develop|feature/deploy-ecs)'
                        }
                    }
                    steps {
                        sh 'rm -rf myG.CMS.School/appsettings.json'
                        configFileProvider([
                                configFile(fileId: 'dev_aws_task_definition', targetLocation: AWS_ECS_TASK_DEFINITION),
                                configFile(fileId: 'dev_appsettings.json', targetLocation: CONFIG_APPSETTINGS),
                        ]) {}
                    }
                }

                stage('Production') {
                    when {
                        expression {
                            BRANCH_NAME ==~ '(production.*|feature/deploy-prod)'
                        }
                    }
                    steps {
                        sh 'rm -rf myG.CMS.School/appsettings.json'
                        configFileProvider([
                                configFile(fileId: 'prod_aws_task_definition', targetLocation: AWS_ECS_TASK_DEFINITION),
                                configFile(fileId: 'prod_appsettings.json', targetLocation: CONFIG_APPSETTINGS),
                        ]) {}
                    }
                }
            }
        }


        stage('Build Docker Image') {
            parallel {
                stage('Develop') {
                    when {
                        expression {
                            BRANCH_NAME ==~ '(develop|feature/deploy-ecs)'
                        }
                    }
                    steps {
                        script {
                            docker.build REPOSITORY_URI + ":dev_${BUILD_DATE}_${BUILD_NUMBER}"
                        }
                    }
                }

                stage('Production') {
                    when {
                        expression {
                            BRANCH_NAME ==~ '(production.*|feature/deploy-prod)'
                        }
                    }
                    steps {
                        script {
                            docker.build REPOSITORY_URI + ":prod_${BUILD_DATE}_${BUILD_NUMBER}"
                        }
                    }
                }
            }
        }


        stage('Push Image to ECR') {
            parallel {
                stage('Develop') {
                    when {
                        expression {
                            BRANCH_NAME ==~ '(develop|feature/deploy-ecs)'
                        }
                    }
                    steps {
                        sh "docker push ${REPOSITORY_URI}:dev_${BUILD_DATE}_${BUILD_NUMBER}"
                    }
                }

                stage('Production') {
                    when {
                        expression {
                            BRANCH_NAME ==~ '(production.*|feature/deploy-prod)'
                        }
                    }
                    steps {
                        sh "docker push ${REPOSITORY_URI}:prod_${BUILD_DATE}_${BUILD_NUMBER}"
                    }
                }
            }
        }


        stage('Deploy in ECS') {
            parallel {
                stage('Develop') {
                    when {
                        expression {
                            BRANCH_NAME ==~ '(develop|feature/deploy-ecs)'
                        }
                    }
                    steps {
                        sh "sed -e 's;%BUILD_NUMBER%;${BUILD_NUMBER};g' -e 's;%BUILD_DATE%;${BUILD_DATE};g' -e 's;%ENV%;dev;g' -e 's;%REPOSITORY_URI%;${REPOSITORY_URI};g' -e 's;%MEMORY%;1024;g' -e 's;%CPU%;1024;g' taskDefinition.json > taskDefinition_${BUILD_NUMBER}.json"

                        sh 'aws ecs register-task-definition --family "dev_gameguild" --cli-input-json file://taskDefinition_${BUILD_NUMBER}.json --region "ap-southeast-1"'

                        script {
                            def revision = sh(
                                    returnStdout: true,
                                    script: 'aws ecs describe-task-definition --task-definition "dev_gameguild" | egrep \'revision\' | tr \',\' \' \' | awk \'{print \$2}\''
                            ).trim()

                            echo "revision ===== ${revision}"

                            sh """
                              aws ecs update-service --cluster ${DEV_CLUSTER_NAME} --service ${SERVICE_NAME} --task-definition dev_gameguild:${revision}
                            """
                        }
                    }
                }

                stage('Production') {
                    when {
                        expression {
                            BRANCH_NAME ==~ '(production.*|feature/deploy-prod)'
                        }
                    }
                    steps {
                        sh "sed -e 's;%BUILD_NUMBER%;${BUILD_NUMBER};g' -e 's;%BUILD_DATE%;${BUILD_DATE};g' -e 's;%ENV%;dev;g' -e 's;%REPOSITORY_URI%;${REPOSITORY_URI};g' -e 's;%MEMORY%;1024;g' -e 's;%CPU%;1024;g' taskDefinition.json > taskDefinition_${BUILD_NUMBER}.json"

                        sh 'aws ecs register-task-definition --family "prod_gameguild" --cli-input-json file://taskDefinition_${BUILD_NUMBER}.json --region "ap-southeast-1"'

                        script {
                            def revision = sh(
                                    returnStdout: true,
                                    script: 'aws ecs describe-task-definition --task-definition "prod_gameguild" | egrep \'revision\' | tr \',\' \' \' | awk \'{print \$2}\''
                            ).trim()

                            sh """
                              aws ecs update-service --cluster ${PROD_CLUSTER_NAME} --service ${SERVICE_NAME} --task-definition prod_gameguild:${revision}
                            """
                        }

                    }
                }
            }
        }
    }
}
